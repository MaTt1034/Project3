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
            Show(product);
        }
        public void Show(Product product)
        {

            _view.ShowSingle(product);
        }


        public void Create()
        {
            _view.DisplayNewForm();

            Product newProduct = new Product(_view.ID, _view.Name, _view.Description, _view.Price);

            Products.Add(newProduct);
            _view.ClearValues();
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
