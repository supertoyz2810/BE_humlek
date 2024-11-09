using HumlekCoffeeBE.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Application.Model.Response.User
{
    public class UserRoleResponse
    {
        public int RoleCode { get; set; }
        public UserRoleStatus Role { get; set; }
    }
}
