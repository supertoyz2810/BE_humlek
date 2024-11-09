using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Application.Model.Response.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }

    public class UserInfoResponse : UserResponse
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserRoleResponse> Roles { get; set; }
    }
}
