using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.ViewModels
{
    public class ShoesListViewModels
    {
        public IEnumerable<Shoes> allShoes { get; set; }
        public string currCategory { get; set; }
    }
}
