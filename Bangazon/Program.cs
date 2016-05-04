using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  class Program
  {
    static void Main(string[] args)
    {
      InvoiceDb db = new InvoiceDb();
      Menu menu = new Menu();

      List<Product> orderProducts = new List<Product>();
      int menuChoice;
      int prodChoice;
      string outputStr = "";
      bool quitProgram = false;



      while (!quitProgram)
      {

        Console.Clear();
        Console.WriteLine("\n*********************************************************");
        Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
        Console.WriteLine("*********************************************************\n");
        Console.WriteLine("1.Create an account");
        Console.WriteLine("2.Create a payment option");
        Console.WriteLine("3.Order a product");
        Console.WriteLine("4.Complete an order");
        Console.WriteLine("5.See product popularity");
        Console.WriteLine("6.Leave Bangazon!\n");
        Console.Write(">");
        Int32.TryParse(Console.ReadLine(), out menuChoice);

        switch (menuChoice)
        {
          case 1: // CREATE AN ACCOUNT
            menu.addCustomer();
            break;
          case 2: // CREATE A PAYMENT OPTION
            menu.addPaymentOption();
            break;
          case 3: // ORDER A PRODUCT
            orderProducts = menu.orderProduct(orderProducts);
            break;
          case 4: // COMPLETE AN ORDER
            menu.completeOrder(orderProducts);
            break;
          case 5: // SEE PRODUCT POPULARITY
            List<PopularProduct> popProds = db.getPopularProducts();
            Console.Clear();
            Console.WriteLine("\n*********************************************************");
            Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
            Console.WriteLine("*********************************************************\n");
            foreach (PopularProduct prod in popProds)
            {
              Console.WriteLine("{0} ordered {1} times by {2} customers for total revenue of ${3}.", prod.name, prod.numOrdered, prod.numCustomers, prod.totalRevenue);
            }
            Console.WriteLine("\nPress enter to return to main menu");
            Console.Read();
            break;
          case 6: // EXIT
            outputStr = "Goodbye!";
            Console.Clear();
            Console.WriteLine("\n*********************************************************");
            Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
            Console.WriteLine("*********************************************************\n");
            Console.WriteLine("1.Create an account");
            Console.WriteLine("2.Create a payment option");
            Console.WriteLine("3.Order a product");
            Console.WriteLine("4.Complete an order");
            Console.WriteLine("5.See product popularity");
            Console.WriteLine("6.Leave Bangazon!");
            Console.WriteLine("\n" + outputStr + "\n");
            quitProgram = true;
            break;
          default:
            outputStr = "Invalid choice. Please choose from the menu above.";
            break;
        } // End Menu Switch
      } // End Menu Loop
    } // End Main
  } // End Program Class
} // End Namespace
