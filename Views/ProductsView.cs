using System;
namespace Project3.Views
{
	public class ProductsView : IView
	{
		public ProductsView()
		{
		}

		public void IndexPage()
		{
			// Shows all products in the database
			// Also redirects to ShowPage(id of the product)
			// Also redirects to NewPage()
        }

		public void ShowPage()
		{
			// Shows the individual record
			// Also redirects to EditPage()
			// Also destroys the record (from controller)
		}

		public void NewPage()
		{
			// Contains input fields
			// Creates a new product, adds in the DB
		}

        public void EditPage()
        {
            // Contains input fields
            // Edits a product, updates DB
        }
    }
}
