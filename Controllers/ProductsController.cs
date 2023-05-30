using System;
using Project3.Models;
using Project3.Views;
using Project3.Database;

namespace Project3.Controllers
{
	public class ProductsController
    {
        private ProductsView _view;
        public List<Product> Products { get; set; }

        public ProductsController()
        {
            _view = new ProductsView();
            Products = new List<Product>();
        }

        public void Show(int id)
        {
            Product product = SetProduct(id);

            _view.ShowSingle(product);
        }

        public void Create(int id, string name, string description, double price)
        {
            Product newProduct = new Product(id, name, description, price);

            Products.Add(newProduct);

            Show(newProduct.ID);
        }

        public void Update(int id, string? name = null, double price = -1)
        {
            Product product = SetProduct(id);

            if (name != null)
                product.Name = name;

            if (price != -1)
                product.Price = price;
        }

        private Product SetProduct(int id)
        {
            return Products.FirstOrDefault(p => p.ID == id);
        }
    }
}
