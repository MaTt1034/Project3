using System;
using Project3.Models;
using Project3.Views;
using Project3.Database;

namespace Project3.Controllers
{
    public class ProductController
    {
        private ProductsView view;
        List<Product> Products { get; set; }

        public IReadOnlyCollection<Product> GetProducts()
        {
            return Products;
        }

        public void AddProduct(Product product)
        {
            if (!Master.DepartmentController.GetDepartments().Contains(product.Department))
                throw new InvalidOperationException("Department does not exist. Add to list of departments first.");
            if (Products.Any(x => x.Id == product.Id)) throw new InvalidOperationException();

            Products.Add(product);
        }
        public bool DropProduct(Product product)
        {
            return Products.Remove(product);
        }

        public ProductController()
        {
            view = new ProductsView();
            Products = new List<Product>();
        }

        public void Show(int id)
        {
            Product product = SetProduct(id);
            Show(product);
        }
        public void Show(Product product)
        {

            view.ShowSingle(product);
        }


        public void Create(Department? department = null)
        {
            view.DisplayNewForm();

            view.Department = department;
            Product newProduct = new Product(view.ID, view.Name, view.Description, view.Price, department);

            Products.Add(newProduct);
            view.ClearValues();
        }

        public void Update(int id, string? name = null, string desc = null, double? price = null)
        {
            Product product = SetProduct(id);

            if (product == null) throw new NullReferenceException();

            product.Name = name ?? product.Name;

            product.Description = desc ?? product.Description;

            product.Price = price == null ? product.Price : (double)price;
        }

        private Product SetProduct(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
