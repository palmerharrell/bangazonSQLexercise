using System;
using System.Collections.Generic;

namespace Bangazon
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Product> orderProducts = new List<Product>();
      int menuChoice;
      string outputStr = "";
      bool quitProgram = false;

      while (!quitProgram)
      {
        outputStr = Menu.displayMainMenu(outputStr);
        Int32.TryParse(Console.ReadLine(), out menuChoice);

        switch (menuChoice)
        {
          case 1: // CREATE AN ACCOUNT
            Menu.addCustomer();
            break;
          case 2: // CREATE A PAYMENT OPTION
            Menu.addPaymentOption();
            break;
          case 3: // ORDER A PRODUCT
            orderProducts = Menu.orderProduct(orderProducts);
            break;
          case 4: // COMPLETE AN ORDER
            outputStr = Menu.completeOrder(orderProducts);
            break;
          case 5: // SEE PRODUCT POPULARITY
            Menu.popularProducts();
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
