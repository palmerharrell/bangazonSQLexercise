using System;
using System.Collections.Generic;

namespace Bangazon
{
  class Program
  {
    static void Main(string[] args)
    {
      Menu menu = new Menu();
      List<Product> orderProducts = new List<Product>();
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
        Console.WriteLine("6.Leave Bangazon!\n");
        Console.WriteLine("{0}\n", outputStr);
        outputStr = "";
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
            outputStr = menu.completeOrder(orderProducts);
            break;
          case 5: // SEE PRODUCT POPULARITY
            menu.popularProducts();
            Console.Read();
            //outputStr = "";
            break;
          case 6: // EXIT
            Console.WriteLine("\nGoodbye!\n");
            quitProgram = true;
            break;
          default:
            break;
        }
      }
    } // End Main
  } // End Program Class
} // End Namespace
