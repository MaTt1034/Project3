using Project3.Models;
using Project3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class DepartmentController : IController<Department, int>
    {
        private DepartmentView view;

        protected List<Department> List;

        public void Add(Department department)
        {
            if (List.Any(x => x.Id == department.Id)) throw new InvalidOperationException();
            List.Add(department);
        }

        public Product[] GetContents(Department department)
        {
            return Master.ProductController.GetList().Where(x => x.Department == department).ToArray();
        }

        public DepartmentController()
        {
            view = new DepartmentView();
            List = new List<Department>();
        }

        public void Show(int id)
        {
            Department item = Set(id);
            Show(item);
        }

        public void Show(Department department)
        {
            view.ShowSingle(department);
        }


        public void Create()
        {
            view.DisplayNewForm();
            Department newDepartment = new Department(view.ID, view.Name, view.Description);
            Add(newDepartment);
            view.ClearValues();
        }
        public void Update(Department temp)
        {
            Department dep = Set(temp.Id);

            dep.Name = temp.Name ?? dep.Name;

            dep.Description = temp.Description ?? dep.Description;
        }

        private Department Set(int id)
        {
            return List.FirstOrDefault(p => p.Id == id);
        }

        public void Index()
        {
            throw new NotImplementedException();
        }

        public bool Destroy(int id)
        {
            return Destroy(List.Find(x => x.Id == id));
        }

        public bool Destroy(Department item)
        {
            if (Master.ProductController.GetList().Any(x => x.Department == item))
                throw new InvalidOperationException("Remove products from inventory before deleting department.");
            return List.Remove(item);
        }

        public IReadOnlyCollection<Department> GetList()
        {
            return List;
        }
    }
}
