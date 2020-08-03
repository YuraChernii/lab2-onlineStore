using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Shoes> favouriteShoes { get; set; }
    }
}
