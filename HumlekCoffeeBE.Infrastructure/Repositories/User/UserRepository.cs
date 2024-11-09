using BCrypt.Net;
using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Interfaces;
using HumlekCoffeeBE.Base.Repositories;
using HumlekCoffeeBE.Domain.Entities;
using HumlekCoffeeBE.Infrastructure.HumlekCoffeeBEDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.Repositories.User
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetByUserNameAsync(string userName);
        Task<UserEntity> GetByUserIdAsync(Guid Id);
        Task<bool> CheckPasswordAsync(UserEntity user, string password);
        Task<bool> UserExistAsync(Guid Id);
    }
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(HCDbContext context, IHttpContextAccessor httpContextAccessor)
           : base(context, httpContextAccessor)
        {
        }
        public async Task<UserEntity> GetByUserNameAsync(string userName)
        {
            if (userName != null)
            {
                return await IQueryable().Include(x => x.UserRoles).FirstOrDefaultAsync(ele => ele.UserName == userName);
            }
            return null;
        }

        public async Task<UserEntity> GetByUserIdAsync(Guid Id)
        {
            return await IQueryable().FirstOrDefaultAsync(ele => ele.Id == Id);
        }

        public async Task<bool> CheckPasswordAsync(UserEntity user, string password)
        {
            if (password != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, user.UserPassword);
            }
            return false;
        }

        public async Task<bool> UserExistAsync(Guid Id)
        {
            return await IQueryable().AnyAsync(x => x.Id == Id);
        }
    }
}
