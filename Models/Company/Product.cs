using System;

namespace Project3.Models
{
	public class Product : IModel
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

		public Product(int id, string name, string description, double price)
		{
            ID = id;
            Name = name;
            Description = description;
            Price = price;
		}
    }
}
