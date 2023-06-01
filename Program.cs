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


        static bool confirmAction(string msg)
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

            // Create View
            ProductsView productView = new ProductsView();

            // Create Controller
            Master.DepartmentController.AddDepartment(department);
            Master.ProductController.AddProduct(product);

            // Display product details
            Master.ProductController.Show(1);

            // Set product details
            Master.ProductController.Update(1, "Product 1", "aaaa", 3.9);

            //// Display product details
            //productsController.Show(1);

            //productsController.Create();

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Gray;
                try
                {
                    var action = ConsoleMenu.Show("[MAIN MENU]", "INVENTORY...", "EXIT...");
                    switch (action)
                    {
                        case 0:
                            productsMenu();
                            break;

                        default:
                            Console.BackgroundColor = ConsoleColor.Black;

                            if (confirmAction("Exit"))
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Clear();

                                return;
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleExt.RunInColor(ConsoleColor.Red, () =>
                    {
                        ConsoleExt.Separator(c: 'X');
                        Console.Error.WriteLine(ex);
                        ConsoleExt.Separator(c: 'X');
                    });
                    ConsoleExt.Pause(true);
                }

            }
            //Console.ReadLine();
        }

        static void productsMenu()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;

                //Get department
                var deps = Master.DepartmentController.GetDepartments().Select(x => $"{x.Name} -- {x.Id}").Prepend(" 8  ALL").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray();
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
                        prodDepartment = Master.DepartmentController.GetDepartments().Where(x => x.Id == prodId).First();
                        getProds = getProds.Where(x=>x.Department == prodDepartment).ToArray();
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
