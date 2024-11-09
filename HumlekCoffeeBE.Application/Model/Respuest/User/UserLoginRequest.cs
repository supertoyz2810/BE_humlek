using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Application.Model.Respuest.User
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginWithGoogleRequest
    {
        public string AccessToken { get; set; }
        public string IDToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
