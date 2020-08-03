using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tymchak_shop.Data.Interfaces;
using Tymchak_shop.Data.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Tymchak_shop.Data.Repository
{
    public class ShoesRepository : IAllShoes
    {
        private readonly AppDBContent appDBContent;
        public ShoesRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Shoes> Shoes => appDBContent.Shoes.Include(c => c.Category);

        public IEnumerable<Shoes> getFavShoes => appDBContent.Shoes.Where(p => p.isFavourite).Include(c => c.Category);

        public Shoes getObjectShoes(int shoesId) => appDBContent.Shoes.FirstOrDefault(p => p.id == shoesId);
       
    }
}
