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
      int menuChoice;
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
        Console.WriteLine("6.Leave Bangazon!");
        Console.WriteLine("\n" + outputStr + "\n");
        Console.Write(">");
        Int32.TryParse(Console.ReadLine(), out menuChoice);

        switch (menuChoice)
        {
          case 1:
            //outputStr = "Choice: Create an account";
            Customer newCustomer = new Customer();
            Console.WriteLine("Enter customer name");
            Console.Write(">");
            newCustomer.name = Console.ReadLine();
            Console.WriteLine("Enter street address");
            Console.Write(">");
            newCustomer.streetAddress = Console.ReadLine();
            Console.WriteLine("Enter city");
            Console.Write(">");
            newCustomer.city = Console.ReadLine();
            Console.WriteLine("Enter state");
            Console.Write(">");
            newCustomer.state = Console.ReadLine();
            Console.WriteLine("Enter postal code");
            Console.Write(">");
            newCustomer.postalCode = Console.ReadLine();
            Console.WriteLine("Enter phone number");
            Console.Write(">");
            newCustomer.phoneNumber = Console.ReadLine();
            db.addCustomer(newCustomer);
            // Console.WriteLine("New Customer Added:");
            // Console.WriteLine(newCustomer.name);
            // Console.WriteLine(newCustomer.streetAddress);
            // Console.WriteLine(newCustomer.city);
            // Console.WriteLine(newCustomer.state);
            // Console.WriteLine(newCustomer.postalCode);
            // Console.WriteLine(newCustomer.phoneNumber);
            // Console.Read();

            break;
          case 2:
            outputStr = "Choice: Create a payment option";
            break;
          case 3:
            outputStr = "Choice: Order a product";
            break;
          case 4:
            outputStr = "Choice: Complete an order";
            break;
          case 5:
            outputStr = "Choice: See product popularity";
            break;
          case 6:
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
        }

        // // ~~~ getProducts TEST ~~~
        // List<Product> allProducts = db.getProducts();
        // foreach(Product prod in allProducts)
        // {
        //   Console.WriteLine("\nID: {0}", prod.idProduct);
        //   Console.WriteLine("Type ID: {0}", prod.idProductType);
        //   Console.WriteLine("Name: {0}", prod.name);
        //   Console.WriteLine("Price: {0}", prod.price);
        //   Console.WriteLine("Description: {0}", prod.description);
        // }

      } // End Menu Loop
    } // End Main
  } // End Program Class
} // End Namespace
