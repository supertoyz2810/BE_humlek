using Azure;
using DnsClient;
using Google.Apis.Auth;
using HumlekCoffeeBE.Application.Model.Response.User;
using HumlekCoffeeBE.Application.Model.Respuest.User;
using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Enum;
using HumlekCoffeeBE.Base.Extensions;
using HumlekCoffeeBE.Base.Query;
using HumlekCoffeeBE.Base.Service;
using HumlekCoffeeBE.Domain.Entities;
using HumlekCoffeeBE.Infrastructure.Repositories.User;
using HumlekCoffeeBE.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using MongoDB.Driver.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HumlekCoffeeBE.Application.Services.User
{
    public interface IUserService
    {
        Task<BaseResponse<UserResponse>> GetByUserNameAsync(string userName);
        Task<BaseResponse<UserInfoResponse>> CreateUserByUserNameAsync(UserNewRequest request);
        Task<BaseResponse<UserLoginResponse>> LoginWithGoogleAsync(UserLoginWithGoogleRequest request);
        Task<BaseResponse<UserLoginResponse>> LoginAsync(UserLoginRequest request);
        Task<BaseResponse<UserInfoResponse>> GetUserInfo();
    }
    public class UserService : BaseService, IUserService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Repository<IUserRepository>();
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        #region GET
        public async Task<BaseResponse<UserResponse>> GetByUserNameAsync(string userName)
        {
            var result = await _userRepository.GetByUserNameAsync(userName);
            if (result == null)
                return null;

            var response = new UserResponse
            {
                Id = result.Id,
                UserName = result.UserName,
            };

            return ConvertResponse(response);
        }

        public async Task<BaseResponse<UserInfoResponse>> GetUserInfo()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id")?.Value;
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null || userName == null)
            {
                return ConvertResponseForError<UserInfoResponse>(Errors.ER003.ToString(), ((Errors)(int)Errors.ER003).EnumDescriptionAttr());
            }

            var userExsist = await _userRepository.UserExistAsync(Guid.Parse(userId));
            if (userExsist)
            {
                return ConvertResponseForError<UserInfoResponse>(Errors.ER003.ToString(), ((Errors)(int)Errors.ER003).EnumDescriptionAttr());
            }

            var user = await _userRepository.GetByUserIdAsync(Guid.Parse(userId));
            var roles = user.UserRoles.Select(x => new UserRoleResponse { RoleCode = (int)x.Role ,Role = x.Role}).ToList();
            var response = new UserInfoResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Roles = roles
            };
            return ConvertResponse(response);
        }
        #endregion

        #region CREATE
        public async Task<BaseResponse<UserInfoResponse>> CreateUserByUserNameAsync(UserNewRequest request)
        {
            var id = Guid.NewGuid();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var userResponse = new UserEntity
                {
                    Id = id,
                    UserName = request.UserName,
                    Note = request.UserName,
                    Email = request.Email,
                    Phone = request.Phone,
                    UserRoles = new List<UserRoleEntity>() { new UserRoleEntity
                    {
                        Id = Guid.NewGuid(),
                        Role = UserRoleStatus.user,
                        UserId = id,
                        CreatedDate = DateTime.Now,
                        CreatedName = request.UserName,
                        CreatedUser = id,
                        UpdatedDate = DateTime.Now,
                        UpdatedName = request.UserName,
                        UpdatedUser = id
                    } }
                };

                userResponse.UserPassword = BCrypt.Net.BCrypt.HashPassword(request.UserName);

                await SetClaimUserLogin(id.ToString(), request.UserName);
                await _userRepository.InsertAsync(userResponse);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return ConvertResponse(new UserInfoResponse
                {
                    Id = id,
                    UserName = request.UserName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Roles = userResponse.UserRoles.Select(x => new UserRoleResponse { Role = x.Role}).ToList()
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ConvertResponseForError<UserInfoResponse>(Errors.ER005.ToString(), ((Errors)(int)Errors.ER005).EnumDescriptionAttr());
            }
        }
        #endregion

        #region Login
        public async Task<BaseResponse<UserLoginResponse>> LoginWithGoogleAsync(UserLoginWithGoogleRequest request)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IDToken);

                // Kiểm tra thông tin người dùng
                var userId = payload.Subject;
                var email = payload.Email;
                var name = email.Substring(0, email.IndexOf('@'));

                var user = await _userRepository.GetByUserNameAsync(name);
                var token = string.Empty;
                if (user == null)
                {
                    var response = await CreateUserByUserNameAsync(new UserNewRequest { UserName = name, Email = email , Phone = ValueEnum.PhoneFake.ToString()});
                    token = await GenToken(response.Data.Id.ToString(), response.Data.UserName, string.Join(",",response.Data.Roles.Select(x => (int)x.Role).ToArray()));
                    return ConvertResponse(new UserLoginResponse { Token = token });
                }

                await SetClaimUserLogin(user.Id.ToString(), user.UserName);
                var userRoles = user.UserRoles.Select(x => (int)x.Role).ToArray();
                token = await GenToken(user.Id.ToString(), user.UserName, string.Join(",", userRoles));
                return ConvertResponse(new UserLoginResponse { Token = token });
            }
            catch (InvalidJwtException ex)
            {
                return ConvertResponseForError<UserLoginResponse>(Errors.ER001.ToString(), ((Errors)(int)Errors.ER001).EnumDescriptionAttr());
            }
        }

        public async Task<BaseResponse<UserLoginResponse>> LoginAsync(UserLoginRequest request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            if (user == null)
                return ConvertResponseForError<UserLoginResponse>(Errors.ER002.ToString(), ((Errors)(int)Errors.ER002).EnumDescriptionAttr());

            if (user.UserRoles == null)
                return ConvertResponseForError<UserLoginResponse>(Errors.ER004.ToString(), ((Errors)(int)Errors.ER004).EnumDescriptionAttr());

            var userRoleAdmin = user.UserRoles.Any(x => x.Role == UserRoleStatus.superAdmin);
            if (!userRoleAdmin)
                return ConvertResponseForError<UserLoginResponse>(Errors.ER004.ToString(), ((Errors)(int)Errors.ER004).EnumDescriptionAttr());

            var checkPassword = await _userRepository.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
                return ConvertResponseForError<UserLoginResponse>(Errors.ER006.ToString(), ((Errors)(int)Errors.ER006).EnumDescriptionAttr());

            await SetClaimUserLogin(user.Id.ToString(), request.UserName);
            var userRoles = user.UserRoles.Select(x => (int)x.Role ).ToArray();
            var token = await GenToken(user.Id.ToString(), user.UserName, string.Join(",", userRoles));
            return ConvertResponse(new UserLoginResponse { Token = token });
        }
        #endregion

        #region Funcion
        private async Task SetClaimUserLogin(string id, string name)
        {
            var claims = new List<Claim>
            {
                new Claim("id", id),
                new Claim(ClaimTypes.Name, name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CustomAuthType");

            // Create a ClaimsPrincipal and assign it to HttpContext.User
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _httpContextAccessor.HttpContext.User = claimsPrincipal;

            // Optional: Sign in the user if you want to persist the claims across requests
            //await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);
        }

        private async Task<string> GenToken(string userId, string userName, string userRoles)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim("role", userRoles),
            };
            // Cấu hình JWT Bearer Authentication
            //var signingKey = Configuration.GetSection("JwtSettings:Key").Value;
            //var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            //var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])); // Khóa bí mật
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _configuration["JwtSettings:Issuer"],
               audience: _configuration["JwtSettings:Audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion


    }
}
