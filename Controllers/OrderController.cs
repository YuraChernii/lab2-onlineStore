using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tymchak_shop.Data.Interfaces;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.Controllers
{
    public class OrderController:Controller
    {
        private readonly IAllOrders allOrders;
        private readonly ShopCart shopCart;
        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            
            shopCart.listShopItems = shopCart.getShopItems();
            if (shopCart.listShopItems.Count == 0)
            {
                ModelState.AddModelError("", "You did not add any shoes to your basket.");
                return View(order);
            }
            if (order.email == null || order.phone == null || order.adress == null) 
                return View(order);
            else 
            {
                if (ModelState.IsValid) 
                {
                    //
                        allOrders.createOrder(order);
                        return RedirectToAction("Complete");
                }
            } 
            
            return View(order);
        }
        public IActionResult Complete()
        {
            ViewBag.Message = "The order successfully greated.";
            return View();
        }
    }
}
