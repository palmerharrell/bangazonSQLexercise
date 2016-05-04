using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class Menu
  {
    public void addCustomer()
    {
      Customer newCustomer = new Customer();
      InvoiceDb db = new InvoiceDb();

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
    }

    public void addPaymentOption()
    {
      PaymentOption pmt = new PaymentOption();
      InvoiceDb db = new InvoiceDb();
      List<Customer> allCustomers = db.getCustomers();
      int numCusts = 0;
      int selectedCust;
      Console.Clear();
      Console.WriteLine("\n*********************************************************");
      Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
      Console.WriteLine("*********************************************************\n");
      Console.WriteLine("Which customer?");

      foreach (Customer cust in allCustomers)
      {
        Console.WriteLine("{0}: {1}", numCusts + 1, cust.name);
        numCusts++;
      }
      Console.Write(">");
      Int32.TryParse(Console.ReadLine(), out selectedCust);
      pmt.idCustomer = allCustomers[selectedCust - 1].idCustomer;
      Console.WriteLine("Enter payment type (e.g. AmEx, Visa, Checking)");
      Console.Write(">");
      pmt.name = Console.ReadLine();
      Console.WriteLine("Enter account number");
      Console.Write(">");
      pmt.accountNumber = Console.ReadLine();
      db.addPmtOption(pmt);
    }

    public List<Product> orderProduct(List<Product> orderProducts)
    {
      //bool productView = true;
      int prodChoice;
      //List<Product> orderProducts = new List<Product>();
      InvoiceDb db = new InvoiceDb();
      while (true)
      {
        List<Product> allProducts = db.getProducts();
        int numProds = 0;

        Console.Clear();
        Console.WriteLine("\n*********************************************************");
        Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
        Console.WriteLine("*********************************************************\n");

        foreach (Product prod in allProducts)
        {
          Console.WriteLine("{0}: {1}", numProds + 1, prod.name);
          numProds++;
        }
        Console.WriteLine("{0}: Back to Main Menu\n", numProds + 1);
        Console.Write(">");
        Int32.TryParse(Console.ReadLine(), out prodChoice);
        if (prodChoice >= numProds + 1)
        {
          return orderProducts;
          //productView = false; // Back to main menu
        }
        else
        {
          orderProducts.Add(allProducts[prodChoice - 1]);
          Console.WriteLine("{0} added to order", allProducts[prodChoice - 1].name);
          System.Threading.Thread.Sleep(1000);
        }
      }
    } // End orderProduct()
  }
}
