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
            Console.ForegroundColor = ConsoleColor.Gray;

            // Create Model
            Product product = new Product(1, "bananas", "yum delicious", 3.33);

            // Create View
            ProductsView productView = new ProductsView();

            // Create Controller

            productsController.Products.Add(product);

            // Display product details
            productsController.Show(1);

            // Set product details
            productsController.Update(1, "Product 1", 19.99);

            //// Display product details
            //productsController.Show(1);

            //productsController.Create();

            while (true)
            {
                try
                {
                    var action = ConsoleMenu.Show("[MAIN MENU]", "INVENTORY...", "ADD ITEM...", "EXIT...");
                    switch (action)
                    {
                        case 0:
                            productsMenu();
                            break;
                        case 1:
                            productsController.Create();
                            break;
                        default:
                            if (confirmAction("Exit"))
                            {
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
                var deps = new string[] { "<-- BACK", "<ALL>", "test, not actual department right now" };
                var dep = ConsoleMenu.Show("INVENTORY > DEPARTMENT", deps);
                if (dep == 0) return;
                var prods = productsController.Products.Select(x => x.Name + " -- " + x.ID).Prepend("<-- BACK").ToArray();


                while (true)
                {
                    var prod = ConsoleMenu.Show($"INVENTORY > DEPARTMENT > {deps[dep]} > PRODUCT", prods);
                    if (prod == 0)
                    {
                        break;
                    }

                    var prodNo = int.Parse(prods[prod].Split("--")[1].Trim());
                    var product = productsController.Products.Find(x => x.ID == prodNo);
                    productsController.Show(product);

                    //
                    ConsoleExt.WriteLine("these actions do nothing right now", ConsoleColor.Red);
                    //

                    var action = ConsoleMenu.ShowInline("ACTION?", " <-- DONE", "\\... EDIT", "[][] DUPLICATE", "[X]  DROP!");
                    switch (action)
                    {
                        case 3:
                            if (confirmAction("Delete??"))
                            {
                                //...
                            }
                            break;
                        default:
                            break;
                    }

                }
            }



        }



    }
}
