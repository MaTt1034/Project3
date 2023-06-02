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
            User admin = new Admin("admin", "Admin", "admin@ayhf.com.au", "password");
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



        // This will automatically fire from MainMenu if the user is null!
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
                    centreText("A Y H F");
                    centreText("ONLINE STORE");
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

                //if logged out, log in
                if (Master.User == null) Login();

                //


                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;

                //get all possible actions
                string[] allActions = { "Exit...", "Logout...", "Inventory...", "test", "Users..." };

                try
                {
                    // split action by whether user is admin
                    string[] actions = Master.User is Admin ? new string[] { allActions[2], allActions[4], allActions[3], allActions[1], allActions[0] } :
                        new string[] { allActions[3], allActions[3], allActions[1], allActions[0] };

                    //show menu
                    var action = ConsoleMenu.Show("[MAIN MENU]", actions);

                    //cast selected action's index to its index within all possible actions, and test it to get the function
                    switch (Array.IndexOf(allActions, actions[action]))
                    {
                        case 2: //inventory
                            EditProductsMenu();
                            break;
                        case 4: //users
                            EditUsersMenu();
                            break;
                        case 0: //quit
                            promptQuit();
                            break;
                        case 1: //log out
                            Console.BackgroundColor = ConsoleColor.Black;
                            if (confirmAction("Logout?")) Master.User = null;
                            break;

                        default: //everything else
                            throw new NotImplementedException();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("We encountered an unexpected error and will return to root!");
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

        static void EditProductsMenu()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;

                //Get department
                var deps = Master.DepartmentController.GetList().Select(x => $"{x.Name} -- {x.Id}").Prepend(" 8  ALL").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray();
                var dep = ConsoleMenu.Show("VIEW/EDIT ITEMS > DEPARTMENT", deps);
                var getProds = Master.ProductController.GetList().ToArray();
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
                        if (dep == 2)
                        {
                            ConsoleExt.Separator(c: 'X');
                            Console.WriteLine("Please go back and select a department first!");
                            ConsoleExt.Separator(c: 'X');
                            ConsoleExt.Pause();
                        }
                        else
                            Master.ProductController.Create(prodDepartment);

                        break;
                    }

                    //Edit product

                    //Select product
                    var prodNo = int.Parse(prods[prod].Split("--")[2].Trim());
                    var product = Master.ProductController.GetList().Where(x => x.Id == prodNo).First();

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

        static void EditUsersMenu()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;

                //Get department
                var types = new string[] { "<--  BACK", " 8  ALL", "Admin", "Customer" };
                var type = ConsoleMenu.Show("VIEW/EDIT USERS > TYPE", types);
                var getUsers = Master.UserController.GetList().ToArray();
                int userType = 0;
                switch (type)
                {
                    case 0:
                        return;
                    case 2:
                        userType = 1;
                        break;
                    case 3:
                        userType = 2;
                        break;
                    default:
                        break;
                }

                if (userType != 0)
                {
                    getUsers = getUsers.Where(x => userType == 1 ? x is Admin : x is Customer).ToArray();
                }

                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Get product
                    var users = getUsers.Select(x => $"{x.Name} -- {x.UserName}").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray(); // List the products (name price id), prepend Add, prepend Back (note prepend reverse order!)

                    var user = ConsoleMenu.Show($"VIEW/EDIT USERS > TYPE > {types[type]} > USER", users);// List the options
                    if (user == 0)//Back
                    {
                        break;
                    }
                    if (user == 1)//Create product
                    {
                        if (userType == 0)
                        {
                            ConsoleExt.Separator(c: 'X');
                            Console.WriteLine("Please go back and select a user type first!");
                            ConsoleExt.Separator(c: 'X');
                            ConsoleExt.Pause();
                        }
                        else
                            Master.UserController.Create(userType == 1);

                        break;
                    }

                    //Edit product

                    //Select product
                    var username = (users[user].Split("--")[1].Trim());
                    var userAcc = Master.UserController.GetList().Where(x => x.UserName == username).First();

                    //Show product
                    ConsoleExt.Separator();
                    Master.UserController.Show(username);

                    //Show options
                    var action = ConsoleMenu.ShowInline("ACTION?", " <-- DONE", "\\... EDIT", "[X]  DROP!");
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
