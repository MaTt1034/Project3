using Project3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Views
{
    internal class DepartmentView
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DepartmentView()
        {
        }

        public void Index(List<Product> products)
        {
        }

        public void DisplayNewForm()
        {
            Console.WriteLine("NEW DEPARTMENT");

            Console.Write("ID: ");
            ID = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            Name = Console.ReadLine();

            Console.Write("Description: ");
            Description = Console.ReadLine();


        }

        public void ShowSingle(Department department)
        {
            Console.WriteLine("Product Details:");
            Console.WriteLine($"Name: {department.Name}");
            Console.WriteLine($"Description: {department.Description}");
            Console.WriteLine();
        }

        public void ClearValues()
        {
            ID = 0;
            Name = "";
            Description = "";
        }

    }
}
