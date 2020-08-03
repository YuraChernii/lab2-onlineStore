using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tymchak_shop.Data.Interfaces;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.Data.Repository
{
    public class OrdersRepository : IAllOrders
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;
        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;
        }
        public void createOrder(Order order)
        {
            order.orderTime = DateTime.Now;
            appDBContent.Order.Add(order);
            appDBContent.SaveChanges();
            var items = shopCart.listShopItems;
            foreach(var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    shoesID = el.shoes.id,
                    orderID = order.id,
                    price = el.shoes.price
                };
                appDBContent.OrderDatail.Add(orderDetail);
                
            }
            appDBContent.SaveChanges();
        }
    }
}
