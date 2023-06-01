using System;

namespace Project3.Models
{
	public class Product : IModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Department Department { get; set; }

		public Product(int id, string name, string description, double price, Department department)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Department = department;
        }
    }
}
