using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.Data.Interfaces
{
    public interface IAllShoes
    {
        IEnumerable<Shoes> Shoes { get; }
        IEnumerable<Shoes> getFavShoes { get; }
        Shoes getObjectShoes(int shoesID);
    }
}
