using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tymchak_shop.Data.Models
{
    public class ShopCart
    {
        private AppDBContent appDBContent;
        public ShopCart(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
            ShopCartId = "-1";
        }
        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopItems { get; set; }
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(context) { ShopCartId = shopCartId };
        }
        public void AddToCart(Shoes shoes)
        {
           
            appDBContent.ShopCartItem.Add(new ShopCartItem { 
                ShopCartId = ShopCartId,
                shoes = shoes,
                price = shoes.price
                });
            appDBContent.SaveChanges();
            
        }
        public void DeleteFromCart(int id)
        {
            ShopCartItem b = appDBContent.ShopCartItem.Find(id);
            appDBContent.ShopCartItem.Remove(b);
            appDBContent.SaveChanges();
        }
        public List<ShopCartItem> getShopItems()
        {
            return appDBContent.ShopCartItem.Where(c => c.ShopCartId == ShopCartId).Include(s => s.shoes).ToList();
        }
    }
}
