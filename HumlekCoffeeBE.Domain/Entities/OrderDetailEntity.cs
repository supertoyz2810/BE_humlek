using HumlekCoffeeBE.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class OrderDetailEntity : BaseEntity
    {
        public Guid FoodId { get; set; }
        public Guid OrderId { get; set; }
        public Guid userId { get; set; }
        public virtual OrderEntity Order { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual FoodEntity Food { get; set; }
    }
}
