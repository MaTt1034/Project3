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
    public class DepartmentController
    {
        private DepartmentView view;

        private List<Department> Departments = new List<Department>();

        public IReadOnlyCollection<Department> GetDepartments()
        {
            return Departments;
        }

        public void AddDepartment(Department department)
        {
            if (Departments.Any(x => x.Id == department.Id)) throw new InvalidOperationException();
            Departments.Add(department);
        }
        public bool DropDepartment(Department department)
        {
            if (Master.ProductController.GetProducts().Any(x => x.Department == department))
                throw new InvalidOperationException("Remove products from inventory before deleting department.");
            return Departments.Remove(department);
        }

        public Product[] GetContents(Department department)
        {
            return Master.ProductController.GetProducts().Where(x => x.Department == department).ToArray();
        }

        public DepartmentController()
        {
            view = new DepartmentView();
            Departments = new List<Department>();
        }

        public void Show(int id)
        {
            Department department = SetDepartment(id);
            Show(department);
        }
        public void Show(Department department)
        {

            view.ShowSingle(department);
        }


        public void Create()
        {
            view.DisplayNewForm();
            Department newDepartment = new Department(view.ID, view.Name, view.Description);
            AddDepartment(newDepartment);
            view.ClearValues();
        }
        public void Update(int id, string? name = null, string? desc = null)
        {
            Department dep = SetDepartment(id);
            if (dep == null) throw new NullReferenceException();

            dep.Name = name ?? dep.Name;

            dep.Description = desc ?? dep.Description;
        }

        private Department SetDepartment(int id)
        {
            return Departments.FirstOrDefault(p => p.Id == id);
        }
    }
}
