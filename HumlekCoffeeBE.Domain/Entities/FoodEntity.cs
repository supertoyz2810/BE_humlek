using HumlekCoffeeBE.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Domain.Entities
{
    public class FoodEntity : BaseEntity
    {
        public Guid RestaurantId { get; set; }
        public Guid CategoryFoodId { get; set; }
        public string FoodName { get; set; }
        public string FoodPrice { get; set; }
        public string FoodLinkImage { get; set; }
        public virtual RestaurantEntity Restaurant { get; set; }
        public virtual CategoryFoodEntity CategoryFood { get; set; }
    }
}
