using Project3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    internal class UserController : IController<User, string>
    {
        private UserView view;
        protected List<User> List;

        public UserController()
        {
            view = new UserView();
            List = new List<User>();
        }
        public void Add(User item)
        {
            if (List.Any(x => x.UserName == item.UserName)) throw new InvalidOperationException();
            List.Add(item);
        }

        public void Create()
        {
            Create(false);
        }

        public void Create(bool isAdmin = false)
        {
            view.DisplayNewForm();
            User newUser = isAdmin ?
                 new Admin(view.UserName, view.Name, view.Email, view.Password)
                 :
                  new Customer(view.UserName, view.Name, view.Email, view.Password);
            Add(newUser);
            view.ClearValues();
        }

        public bool Destroy(string id)
        {
            return Destroy(List.Find(x => x.UserName == id));
        }

        public bool Destroy(User item)
        {
            return List.Remove(item);
        }

        public void Index()
        {
            throw new NotImplementedException();
        }

        public void Show(User item)
        {
            view.ShowSingle(item);
        }

        public void Show(string id)
        {
            User item = Set(id);
            Show(item);
        }

        private User Set(string id)
        {
            return List.FirstOrDefault(p => p.UserName == id);
        }


        public void Update(User temp)
        {
            User dep = Set(temp.UserName);

            dep.Name = temp.Name ?? dep.Name;
            dep.Email = temp.Email ?? dep.Email;
            dep.Password = temp.Password ?? dep.Password;

        }

        public IReadOnlyCollection<User> GetList()
        {
            return (IReadOnlyCollection<User>)List;
        }


    }
}
