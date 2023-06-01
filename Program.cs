using System;
using Project3.Controllers;
using Project3.Models;
using Project3.Views;
using StuffProject.ConsoleExt;

namespace Project3
{
    public class Program
    {
        static ProductsController productsController = new ProductsController();


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
            Product product = new Product(1, "bananas", "yum delicious", 3.33);

            // Create View
            ProductsView productView = new ProductsView();

            // Create Controller

            productsController.Products.Add(product);

            // Display product details
            productsController.Show(1);

            // Set product details
            productsController.Update(1, "Product 1", 3.9);

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
                var deps = new string[] { "<-- BACK", " +  ADD...", "<ALL>", "test, not actual department right now" };
                var dep = ConsoleMenu.Show("VIEW/EDIT ITEMS > DEPARTMENT", deps);
                if (dep == 0) return;
                if (dep == 1) //Create department
                {
                    //...
                    throw new NotImplementedException();

                    continue;
                }


                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    //Get product
                    var prods = productsController.Products.Select(x => $"{x.Name} -- ${x.Price:0.00} -- {x.ID}").Prepend(" +  ADD...").Prepend("<-- BACK").ToArray(); // List the products (name price id), prepend Add, prepend Back (note prepend reverse order!)

                    var prod = ConsoleMenu.Show($"VIEW/EDIT ITEMS > DEPARTMENT > {deps[dep]} > PRODUCT", prods);// List the options
                    if (prod == 0)//Back
                    {
                        break;
                    }
                    if (prod == 1)//Create product
                    {
                        productsController.Create();

                        continue;
                    }

                    //Edit product

                    //Select product
                    var prodNo = int.Parse(prods[prod].Split("--")[2].Trim());
                    var product = productsController.Products.Find(x => x.ID == prodNo);

                    //Show product
                    ConsoleExt.Separator();
                    productsController.Show(product);
                  
                    //Show options
                    var action = ConsoleMenu.ShowInline("ACTION?", " <-- DONE", "[]12 STOCKTAKE", "\\... EDIT", "[][] DUPLICATE", "[X]  DROP!");
                    switch (action)
                    {
                        case 0:
                            break;
                        case 4:
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
