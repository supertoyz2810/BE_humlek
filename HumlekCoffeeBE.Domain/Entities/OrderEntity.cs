using HumlekCoffeeBE.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
        public string OrderCode { get; set; }
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
