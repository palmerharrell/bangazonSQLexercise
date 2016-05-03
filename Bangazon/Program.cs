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
        //Console.WriteLine("\n" + outputStr + "\n");
        Console.Write(">");
        Int32.TryParse(Console.ReadLine(), out menuChoice);

        switch (menuChoice)
        {
          case 1: // CREATE AN ACCOUNT
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
            outputStr = "Customer added";
            break;
          case 2: // CREATE A PAYMENT OPTION
            outputStr = "Choice: Create a payment option";
            break;
          case 3: // ORDER A PRODUCT
            List<Product> allProducts = db.getProducts();
            int numProds = 0;

            Console.Clear();
            Console.WriteLine("\n*********************************************************");
            Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
            Console.WriteLine("*********************************************************\n");

            foreach (Product prod in allProducts)
            {
              Console.WriteLine("{0}: {1}", numProds+1, prod.name);
              numProds++;
            }
            Console.WriteLine("{0}: Back to Main Menu\n", numProds+1);
            Console.Write(">");
            Int32.TryParse(Console.ReadLine(), out prodChoice);
            if (prodChoice == numProds+1)
            {
              break; // Back to main menu
            }
            else
            {
              // Add product to list of products for order (orderProducts or something)
              // Loop back to product menu somehow
              Console.WriteLine("You chose: {0}", allProducts[prodChoice - 1].name);
              Console.Read();
            }
            break;
          case 4: // COMPLETE AN ORDER
            outputStr = "Choice: Complete an order";
            break;
          case 5: // SEE PRODUCT POPULARITY
            outputStr = "Choice: See product popularity";
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
