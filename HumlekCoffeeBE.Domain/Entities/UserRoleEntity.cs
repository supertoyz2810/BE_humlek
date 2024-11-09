using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class UserRoleEntity: BaseEntity
    {
        public UserRoleStatus Role { get; set; }
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
