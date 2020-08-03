using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Tymchak_shop.Data.Models;

namespace Tymchak_shop.Data
{
    public class DBObjects
    {
        public static void Initial (AppDBContent content)
        {
            
            if (!content.Category.Any())
                content.Category.AddRange(Categories.Select(c => c.Value));

            if (!content.Shoes.Any())
            {
                content.AddRange
                    (
                new Shoes
                {
                    name = "Nike-1",
                    longDescription = "",
                    shortDescription = "sdfgd",
                    image = "/img/Nike_1.png",
                    price = 500,
                    isFavourite = true,
                    available = true,
                    Category = Categories["sneakers"]
                },
                    new Shoes
                    {
                        name = "Gucci",
                        longDescription = "",
                        shortDescription = "",
                        image = "/img/Gucci.jpg",
                        price = 1000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["heels"]
                    },
                    new Shoes
                    {
                        name = "Nike-2",
                        longDescription = "",
                        shortDescription = "",
                        image = "/img/Nike_2.webp",
                        price = 600,
                        isFavourite = true,
                        available = true,
                        Category = Categories["sneakers"]
                    },
                    new Shoes
                    {
                        name = "Adidas",
                        longDescription = "",
                        shortDescription = "",
                        image = "/img/Gucci.jpg",
                        price = 550,
                        isFavourite = true,
                        available = true,
                        Category = Categories["sneakers"]
                    }
                    );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if(category == null)
                {
                    var list = new Category[]
                    {
                         new Category{name = "sneakers", description = "modern and comfortable"},
                         new Category{name = "heels", description="trendy"}
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category el in list)
                        category.Add(el.name, el);
                }
                return category;
            }
        }
    }
}
