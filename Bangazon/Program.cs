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

      // Main Menu
      Console.WriteLine("\n*********************************************************");
      Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
      Console.WriteLine("*********************************************************\n");
      Console.WriteLine("1.Create an account");
      Console.WriteLine("2.Create a payment option");
      Console.WriteLine("3.Order a product");
      Console.WriteLine("4.Complete an order");
      Console.WriteLine("5.See product popularity");
      Console.WriteLine("6.Leave Bangazon!");
      Console.Write(">");
      Console.Read();

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


    } // Main
  } // Program Class
} // Namespace
