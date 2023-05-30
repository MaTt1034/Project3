using System;
using Project3.Models;
using Project3.Views;

namespace Project3.Controllers
{
	public class ProductsController : IController
	{
        private IModel _model;
        private IView _view;

		public ProductsController()
		{
            _model = new Product();
            _view = new ProductsView();
		}

        public void Index()
        {
            List<string> products = new List<string> { "banana", "chocolate" };

            // redirects to the index view page
            _view.ShowIndexPage(products);
        }








        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Destroy(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public void Show(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}

