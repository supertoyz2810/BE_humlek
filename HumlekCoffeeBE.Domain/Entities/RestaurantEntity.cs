using HumlekCoffeeBE.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class RestaurantEntity : BaseEntity
    {
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantLinkImage { get; set; }
        public virtual ICollection<FoodEntity> Foods { get; set; }
    }
}
