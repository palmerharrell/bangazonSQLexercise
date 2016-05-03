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
        //Console.WriteLine("\n" + outputStr + "\n");
        Console.Write(">");
        Int32.TryParse(Console.ReadLine(), out menuChoice);
        //menuChoice = int.Parse(Console.ReadLine());

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
            //outputStr = "Choice: Create a payment option";
            PaymentOption pmt = new PaymentOption();
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
            outputStr = "Payment method added";
            break;
          case 3: // ORDER A PRODUCT
            bool productView = true;
            while (productView)
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
                productView = false; // Back to main menu
              }
              else
              {
                // Add product to list of products for order (orderProducts or something)
                orderProducts.Add(allProducts[prodChoice - 1]);
                Console.WriteLine("{0} added to order", allProducts[prodChoice - 1].name);
                System.Threading.Thread.Sleep(1000);
                
              }
            }

            break;
          case 4: // COMPLETE AN ORDER
            if (orderProducts.Count < 1)
            {
              Console.WriteLine("\nPlease add some products to your order first. Press any key to return to main menu.");
              Console.Read();
              break;
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
                numCusts = 0;
                allCustomers = db.getCustomers();
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
                List<PaymentOption> paymentOptions = db.getPmtOptions(currOrder.idCustomer);
                int numOpts = 0;
                foreach (PaymentOption opt in paymentOptions)
                {
                  Console.WriteLine("{0}: {1}", numOpts+1, opt.name);
                  numOpts++;
                }
                Console.Write(">");
                currOrder.idPaymentOption = int.Parse(Console.ReadLine());
                db.addOrder(currOrder.idCustomer, currOrder.idPaymentOption, orderProducts);
                Console.WriteLine("Your order is complete! Press any key to return to main menu.");
                orderProducts.Clear();
                Console.Read();
                break;
              }
              else
              {
                break; // back to main menu
              }
            }
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
