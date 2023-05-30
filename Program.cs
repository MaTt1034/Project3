using System;
using Project3.Controllers;
using Project3.Models;
using Project3.Views;

namespace Project3
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create Model
            Product product = new Product(1, "bananas", "yum delicious", 3.33);

            // Create View
            ProductsView productView = new ProductsView();

            // Create Controller
            ProductsController productsController = new ProductsController();
            productsController.Products.Add(product);

            // Display product details
            productsController.Show(1);

            // Set product details
            productsController.Update(1, "Product 1", 19.99);

            // Display product details
            productsController.Show(1);

            productsController.Create();

            Console.ReadLine();
        }
    }
}
