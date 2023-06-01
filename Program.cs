using System;
using System.Runtime.InteropServices;
using Project3.Controllers;
using Project3.Models;
using Project3.Views;
using StuffProject.ConsoleExt;

namespace Project3
{
    public class Program
    {


        public static bool confirmAction(string msg)
        {
            int output = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            output = ConsoleMenu.ShowInlineSimple("ARE YOU SURE? -- " + msg, "NO", "YES");
            Console.ForegroundColor = ConsoleColor.Gray;
            return output == 1;
        }

        static void Main(string[] args)
        {


            // Create Model
            Department department = new Department(1, "Fresh Food");
            Product product = new Product(1, "bananas", "yum delicious", 3.33, department);
            User admin = new Admin("admin", "Admin", "admin@ayke.com.au", "password");
            User user = new Customer("user", "User", "user@gmail.com", "password2");

            // Create View
            ProductView productView = new ProductView();

            // Create Controller
            //
            Master.DepartmentController.Add(department);
            Master.ProductController.Add(product);
            Master.UserController.Add(admin);
            Master.UserController.Add(user);

            // Display product details
            //  Master.ProductController.Show(1);

            // Set product details
            Master.ProductController.Update(new Product(1, "Product 1", "aaa", 1.20, null));

            //// Display product details
            //productsController.Show(1);

            //productsController.Create();

            //Console.ReadLine();

            MainMenu();
        }
        static void Login()
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();

                    //welcome
                    ConsoleExt.Separator(c: '#');
                    centreText("WELCOME TO THE");
                    centreText("A Y K E");
                    centreText("STORE");
                    ConsoleExt.Separator(c: '#');

                    //get login
                    Console.WriteLine("Username (blank to exit): ");
                    var username = Console.ReadLine();
                    if (username == "")
                    {
                        promptQuit();
                        continue;

                    }
                    Console.WriteLine("Password: ");
                    var password = Console.ReadLine();


                    //check if login is correct
                    if (!Master.UserController.GetList().Any(x => x.UserName == username && x.Password == password)) throw new Exception("The username or password is incorrect");

                    User? user = Master.UserController.GetList().First(x => x.UserName == username && x.Password == password);

                    //log in
                    Master.User = user;
                    return;

                }
                catch (Exception ex)
                {
                    showError(ex);
                }
            }

        }

        private static void centreText(string msg)
        {
            Console.CursorLeft = 1 + ((Console.WindowWidth - 1 - msg.Length) / 2);
            Console.WriteLine(msg);

        }



        //

        static void MainMenu()
        {
            while (true)
            {
                if (Master.User == null) Login();

                //


                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;

                string[] allActions = { "Exit...", "Logout...", "Inventory..." , "test perspective" };

                try
                {
                    // split action by whether user is admin
                    string[] actions = Master.User is Admin ? new string[] { allActions[2], allActions[3], allActions[1], allActions[0] } :
                        new string[] { allActions[3], allActions[3], allActions[1], allActions[0]};

                    var action = ConsoleMenu.Show("[MAIN MENU]", actions);

                    //get action name
                    switch (Array.IndexOf(allActions, actions[action]))
                    {
                        case 2:
                            productsMenu();
                            break;
                        case 0:
                            promptQuit();
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            if (confirmAction("Logout?")) Master.User = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception ex)
                {
                    showError(ex);
                }

            }

        }
        static void showError(Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            ConsoleExt.RunInColor(ConsoleColor.Red, () =>
            {
                ConsoleExt.Separator(c: 'X');
                Console.Error.WriteLine(ex); //
                ConsoleExt.Separator(c: 'X');
            });
            ConsoleExt.Pause(true);
        }

        static void promptQuit()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            if (confirmAction("Exit"))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();

                Environment.Exit(0);
            }
        }

        static void productsMenu()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;

                //Get department
                var deps = Master.DepartmentController.GetList().Select(x => $"{x.Name} -- {x.Id}").Prepend(" 8  ALL").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray();
                var dep = ConsoleMenu.Show("VIEW/EDIT ITEMS > DEPARTMENT", deps);
                var getProds = Master.ProductController.GetProducts().ToArray();
                Department prodDepartment = null;
                switch (dep)
                {
                    case 0:
                        return;
                    case 1:
                        Master.DepartmentController.Create();
                        continue;
                    case 2:
                        break;
                    default:
                        int prodId = int.Parse(deps[dep].Split("--")[1].Trim());
                        prodDepartment = Master.DepartmentController.GetList().Where(x => x.Id == prodId).First();
                        getProds = getProds.Where(x => x.Department == prodDepartment).ToArray();
                        break;
                }

                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Get product
                    var prods = getProds.Select(x => $"{x.Name} -- ${x.Price:0.00} -- {x.Id}").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray(); // List the products (name price id), prepend Add, prepend Back (note prepend reverse order!)

                    var prod = ConsoleMenu.Show($"VIEW/EDIT ITEMS > DEPARTMENT > {deps[dep]} > PRODUCT", prods);// List the options
                    if (prod == 0)//Back
                    {
                        break;
                    }
                    if (prod == 1)//Create product
                    {
                        Master.ProductController.Create(prodDepartment);

                        continue;
                    }

                    //Edit product

                    //Select product
                    var prodNo = int.Parse(prods[prod].Split("--")[2].Trim());
                    var product = Master.ProductController.GetProducts().Where(x => x.Id == prodNo).First();

                    //Show product
                    ConsoleExt.Separator();
                    Master.ProductController.Show(product);

                    //Show options
                    var action = ConsoleMenu.ShowInline("ACTION?", " <-- DONE", "[]12 STOCKTAKE", "\\... EDIT", "[X]  DROP!");
                    switch (action)
                    {
                        case 0:
                            break;
                        case 3:
                            if (confirmAction("Delete??"))
                            {
                                //...
                                throw new NotImplementedException();

                            }
                            break;
                        default:
                            throw new NotImplementedException();

                    }

                }
            }



        }



    }
}
