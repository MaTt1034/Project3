using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Views
{
    internal class UserView : IView<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public UserView() { }
        public void IndexPage()
        {
            throw new NotImplementedException();
        }

        public void DisplayNewForm()
        {
            Console.WriteLine("NEW USER");

            Console.Write("ID: ");
            UserName = Console.ReadLine();

            Console.Write("Name: ");
            Name = Console.ReadLine();

            Console.Write("Email: ");
            Email = Console.ReadLine();


        }

        public void ShowSingle(User item)
        {
            Console.WriteLine("Product Details:");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Email: {item.Email}");
            Console.WriteLine();
        }

        public void ClearValues()
        {
            Name = "";
            Email = "";
            Password = "";
            UserName = "";
        }
    }
}
