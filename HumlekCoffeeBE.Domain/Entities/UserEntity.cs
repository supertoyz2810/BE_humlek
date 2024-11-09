using HumlekCoffeeBE.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class UserEntity: BaseEntity
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
