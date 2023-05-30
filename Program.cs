using System;
using Project3.Controllers;

namespace Project3
{
    public class Program
    {
        static void Main(string[] args)
        {
            IController productsController = new ProductsController();

            productsController.Index();
        }
    }
}
