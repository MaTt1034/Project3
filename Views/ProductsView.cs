using System;
using Project3.Models;

namespace Project3.Views
{
    public class ProductView : IView<Product>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Department? Department { get; set; }

        public ProductView()
        {
        }

        public void Index(List<Product> products)
        {
        }

        public void DisplayNewForm()
        {
            Console.WriteLine("NEW PRODUCT");

            Console.Write("ID: ");
            ID = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            Name = Console.ReadLine();

            Console.Write("Description: ");
            Description = Console.ReadLine();

            Console.Write("Price: ");
            Price = double.Parse(Console.ReadLine());
        }

        public void ShowSingle(Product product)
        {
            Console.WriteLine("Product Details:");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: {product.Price:0.00}");
            Console.WriteLine();
        }

        public void ClearValues()
        {
            ID = 0;
            Name = "";
            Description = "";
            Price = 0;
        }

        public void IndexPage()
        {
            throw new NotImplementedException();
        }
    }
}
