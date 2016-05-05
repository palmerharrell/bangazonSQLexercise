using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class Menu
  {
    public string displayMainMenu(string outputStr)
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
      return outputStr;
    }

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
      //db.addCustomer(newCustomer);
      Customer.addCustomer(newCustomer);
    }

    public void addPaymentOption()
    {
      PaymentOption pmt = new PaymentOption();
      InvoiceDb db = new InvoiceDb();
      //List<Customer> allCustomers = db.getCustomers();
      List<Customer> allCustomers = Customer.getCustomers();
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
      //db.addPmtOption(pmt);
      PaymentOption.addPmtOption(pmt);
    }

    public List<Product> orderProduct(List<Product> orderProducts)
    {
      //bool productView = true;
      int prodChoice;
      //List<Product> orderProducts = new List<Product>();
      InvoiceDb db = new InvoiceDb();
      while (true)
      {
        //List<Product> allProducts = db.getProducts();
        List<Product> allProducts = Product.getProducts();
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
    }

    public string completeOrder(List<Product> orderProducts)
    {
      InvoiceDb db = new InvoiceDb();
      if (orderProducts.Count < 1)
      {
        //Console.WriteLine("\nPlease add some products to your order first. Press any key to return to main menu.");
        //Console.Read();
        return "Please add some products to your order first.";
      }
      else
      {
        float orderTotal = 0;
        Order currOrder = new Order();
        foreach (Product prod in orderProducts)
        {
          orderTotal += prod.price;
        }
        Console.WriteLine("Your order total is ${0}. Ready to purchase", orderTotal);
        Console.Write("(Y/N) >");
        if (Console.ReadLine().ToUpper() == "Y")
        {
          int numCusts = 0;
          int selectedCust;
          //List<Customer> allCustomers = db.getCustomers();
          List<Customer> allCustomers = Customer.getCustomers();
          Console.WriteLine("Which customer is placing the order?");
          foreach (Customer cust in allCustomers)
          {
            Console.WriteLine("{0}: {1}", numCusts + 1, cust.name);
            numCusts++;
          }
          Console.Write(">");
          Int32.TryParse(Console.ReadLine(), out selectedCust);
          currOrder.idCustomer = allCustomers[selectedCust - 1].idCustomer;
          Console.WriteLine("Choose a payment option");
          //List<PaymentOption> paymentOptions = db.getPmtOptions(currOrder.idCustomer);
          List<PaymentOption> paymentOptions = PaymentOption.getPmtOptions(currOrder.idCustomer);
          int numOpts = 0;
          foreach (PaymentOption opt in paymentOptions)
          {
            Console.WriteLine("{0}: {1}", numOpts + 1, opt.name);
            numOpts++;
          }
          Console.Write(">");
          currOrder.idPaymentOption = int.Parse(Console.ReadLine());
          //db.addOrder(currOrder.idCustomer, currOrder.idPaymentOption, orderProducts);
          Order.addOrder(currOrder.idCustomer, currOrder.idPaymentOption, orderProducts);
          Console.WriteLine("Your order is complete! Press any key to return to main menu.");
          orderProducts.Clear();
          Console.Read();
          return "";
        }
        else
        {
          return ""; // back to main menu
        }
      }
    }

    public void popularProducts()
    {
      InvoiceDb db = new InvoiceDb();
      //List<PopularProduct> popProds = db.getPopularProducts();
      List<PopularProduct> popProds = Product.getPopularProducts();
      Console.Clear();
      Console.WriteLine("\n*********************************************************");
      Console.WriteLine("**  Welcome to Bangazon! Command Line Ordering System  **");
      Console.WriteLine("*********************************************************\n");
      foreach (PopularProduct prod in popProds)
      {
        Console.WriteLine("{0} ordered {1} times by {2} customers for total revenue of ${3}.", prod.name, prod.numOrdered, prod.numCustomers, prod.totalRevenue);
      }
      Console.WriteLine("\nPress enter to return to main menu");
    }
  }
}
