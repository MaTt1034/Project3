using System;
using Project3.Models;
using Project3.Controllers;

namespace Project3.Views
{
    public class ProductsView
    {
        public ProductsView()
        {
        }

        public void Index(List<Product> products)
        {
        }

        public void ShowSingle(Product product)
        {
            Console.WriteLine("Product Details:");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine();
        }
    }
}