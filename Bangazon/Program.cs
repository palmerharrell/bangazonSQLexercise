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

      // ~~~ getProducts TEST ~~~
      List<Product> allProducts = db.getProducts();
      foreach(Product prod in allProducts)
      {
        Console.WriteLine("\nID: {0}", prod.idProduct);
        Console.WriteLine("Type ID: {0}", prod.idProductType);
        Console.WriteLine("Name: {0}", prod.name);
        Console.WriteLine("Price: {0}", prod.price);
        Console.WriteLine("Description: {0}", prod.description);
      }

      
    } // Main
  } // Program Class
} // Namespace
