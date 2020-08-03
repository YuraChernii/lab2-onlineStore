using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tymchak_shop.Data.Interfaces;
using Tymchak_shop.Data.Models;
using Tymchak_shop.ViewModels;

namespace Tymchak_shop.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IAllShoes _allShoes;
        private readonly IShoesCategory _shoesCategory;
        public ShoesController(IAllShoes _allShoes, IShoesCategory _shoesCategory)
        {
            this._allShoes = _allShoes;
            this._shoesCategory = _shoesCategory;
        }
        [Route("Shoes/List")]
        [Route("Shoes/List/{category}")]
        public ViewResult List(string category)
        {
            //string _category = category;
            IEnumerable<Shoes> shoes = null;
            string currentCategory = "";
            if (string.IsNullOrEmpty(category))
            {
                shoes = _allShoes.Shoes.OrderBy(i => i.id);
            }
            else
            {
                if (string.Equals("sneakers", category, StringComparison.OrdinalIgnoreCase))
                {
                    shoes = _allShoes.Shoes.Where(i => i.Category.name.Equals("sneakers")).OrderBy(i => i.id);
                }
                else if (string.Equals("heels", category, StringComparison.OrdinalIgnoreCase))
                {
                    shoes = _allShoes.Shoes.Where(i => i.Category.name.Equals("heels")).OrderBy(i => i.id);
                }
                currentCategory = category;
            }
            var shoesObj = new ShoesListViewModels
            {
                allShoes = shoes,
                currCategory = currentCategory
            };
            ViewBag.Title = "The page with shoes";
            return View(shoesObj);
        }
    }
}
