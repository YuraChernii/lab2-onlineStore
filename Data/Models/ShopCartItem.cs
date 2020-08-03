using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tymchak_shop.Data.Models
{
    public class ShopCartItem
    {
        public int id { get; set; }
        public Shoes shoes { get; set; }
        public int price { get; set; }
        public string ShopCartId { get; set; }
    }
}
