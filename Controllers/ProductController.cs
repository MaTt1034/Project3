using System;
using Project3.Models;
using Project3.Views;
using Project3.Database;

namespace Project3.Controllers
{
    public class ProductController : IController<Product,int>
    {
        private ProductView view;
        List<Product> List;

        public void Add(Product item)
        {
            if (!Master.DepartmentController.GetList().Contains(item.Department))
                throw new InvalidOperationException("Department does not exist. Add to list of departments first.");
            if (List.Any(x => x.Id == item.Id)) throw new InvalidOperationException();

            List.Add(item);
        }

        public ProductController()
        {
            view = new ProductView();
            List = new List<Product>();
        }

        public void Show(int id)
        {
            Product product = Set(id);
            Show(product);
        }
        public void Show(Product item)
        {

            view.ShowSingle(item);
        }


        public void Create(Department? department = null)
        {
            view.DisplayNewForm();

            view.Department = department;
            Product newProduct = new Product(view.ID, view.Name, view.Description, view.Price, view.Department);

            List.Add(newProduct);
            view.ClearValues();
        }

        public void Update(Product temp)
        {
            Product product = Set(temp.Id);

            product.Name = temp.Name ?? product.Name;

            product.Description = temp.Description ?? product.Description;

            product.Price = temp.Price == null ? product.Price : (double)temp.Price;
        }

        private Product Set(int id)
        {
            return List.FirstOrDefault(p => p.Id == id);
        }

        public void Index()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            Create(null);
        }

        public bool Destroy(int id)
        {
            return Destroy(List.Find(x => x.Id == id));
        }

        public bool Destroy(Product item)
        {
            return List.Remove(item);
        }

        public IReadOnlyCollection<Product> GetList()
        {
            return (IReadOnlyCollection<Product>)List;
        }
    }
}
