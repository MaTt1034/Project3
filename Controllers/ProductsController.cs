using System;
using Project3.Models;
using Project3.Views;
using Project3.Database;

namespace Project3.Controllers
{
	public class ProductsController : IController
	{
        private Product _model;
        private ProductsView _view;
        private List<Product> _products;

		public ProductsController()
		{
            _model = new Product();
            _view = new ProductsView();
            _products = new List<Product>();
		}

        public void Index()
        {
            _view.IndexPage(_products);
        }

        public void Show(int id)
        {
        }

        public void Create()
        {
            Product newProduct = new Product();

            Show(newProduct.id);
        }

        public void Update(int id)
        {
        }

        public void Destroy(int id)
        {
        }
    }
}
