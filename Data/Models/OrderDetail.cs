using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tymchak_shop.Data.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderID { get; set; }
        public uint price { get; set; }
        public int shoesID { get; set; }
        public virtual Shoes shoes { get; set; }
        public virtual Order order { get; set; }
    }
}
