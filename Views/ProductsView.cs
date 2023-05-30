using System;
namespace Project3.Views
{
	public class ProductsView : IView
	{
		public ProductsView()
		{
		}

		public void ShowIndexPage(List<string> items)
		{
			Console.WriteLine("Showing Products:");

            foreach (string item in items)
            {
                Console.WriteLine(item);
            }
        }
	}
}
