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
    public class ShopCartController : Controller
    {
        private readonly IAllShoes _shoesRepository;
        private readonly ShopCart _shopCart;

        public ShopCartController(IAllShoes shoesRepository, ShopCart shopCart)
        {
            _shoesRepository = shoesRepository;
            _shopCart = shopCart;
        }
        public RedirectToActionResult CheckOut()
        {
            
            if (_shopCart.getShopItems().Count == 0)
                return RedirectToAction("Error");
            return RedirectToAction("Index");
        }
        public ViewResult Error()
        {
            return View();
        }

        public ViewResult Index()
        {
            
            var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;
            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart
            };
            //obj.shopCart.listShopItems = items;
            return View(obj);
        }
        public RedirectToActionResult DeleteToCart(int id)
        {
            _shopCart.DeleteFromCart(id);
            return RedirectToAction("Index");
        }
        public RedirectToActionResult addToCart(int id)
        {
            var item = _shoesRepository.Shoes.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }
    }
}
