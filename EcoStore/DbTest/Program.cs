using EcoStore.EFCore.Context;
using EcoStore.EFCore.Entities;
using System;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product()
            {
                Id = 1,
                Title = "Title",
                Price = 10
            };

            var context = new EcoStoreContext();
            context.Product.Add(product);
            context.SaveChanges();
        }
    }
}
