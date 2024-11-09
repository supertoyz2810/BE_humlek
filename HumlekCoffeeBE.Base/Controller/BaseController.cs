using HumlekCoffeeBE.Base.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected string CurrentUserName
        {
            get
            {
                return (HttpContext != null
                    && HttpContext.User != null
                    && HttpContext.User.Identity.IsAuthenticated) ?
                    HttpContext.User.FindFirst(ClaimTypes.Name).Value : string.Empty;
            }
        }

        protected Guid CurrentId
        {
            get
            {
                return (HttpContext != null
                    && HttpContext.User != null
                    && HttpContext.User.Identity.IsAuthenticated) ?
                    Guid.Parse(HttpContext.User.FindFirst("Id").Value) : Guid.Empty;
            }
        }

        protected List<string> CurrentRoles
        {
            get
            {
                var roleClaims = (HttpContext != null && HttpContext.User != null && HttpContext.User.Identity.IsAuthenticated) ?
                    HttpContext.User.FindAll(ClaimTypes.Role).ToList() : new List<Claim>();

                var roles = new List<string>();
                foreach (var role in roleClaims)
                    roles.Add(role.Value);

                return roles;
            }
        }

        protected IActionResult Result<T>(IBaseResponse<T> response) where T : class
        {
            if (response.MetaData.Status == BaseResponseStatus.ok.ToString())
                return Ok(response);

            if (response.MetaData.Status == BaseResponseStatus.fail.ToString()
                && response.MetaData.ErrorCode == StatusCodes.Status404NotFound.ToString())
                return NotFound(response);

            if (response.MetaData.Status == BaseResponseStatus.fail.ToString()
                && response.MetaData.ErrorCode == StatusCodes.Status403Forbidden.ToString())
                return StatusCode(StatusCodes.Status403Forbidden, response);

            return BadRequest(response);
        }
    }
}
