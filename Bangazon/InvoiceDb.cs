using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class InvoiceDb
  {
    
    public List<Product> getProducts() // read from db, return list of all products
    {
      string query = @"
        SELECT * FROM Product
        ORDER BY Product.Name
      ";

      List<Product> productList = new List<Product>();

      using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True"))
      using (SqlCommand cmd = new SqlCommand(query, connection))
      {
        connection.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read()) // Read advances to the next row.
            {
              // Construct Product object and add to list
              Product currProd = new Product();
              currProd.idProduct = (int)reader[0];
              currProd.idProductType = (int)reader[1];
              currProd.name = (string)reader[2];
              currProd.price = (float)reader[3];
              currProd.description = (string)reader[4];
              productList.Add(currProd);
            }
          }
        }
      }
      return productList;
    } // End getProducts()

    public List<Customer> getCustomers()
    {
      string query = @"
        SELECT * FROM Customer
        ORDER BY Customer.Name
      ";

      List<Customer> customerList = new List<Customer>();

      using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True"))
      using (SqlCommand cmd = new SqlCommand(query, connection))
      {
        connection.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read()) // Read advances to the next row.
            {
              Customer currCust = new Customer();
              currCust.idCustomer = (int)reader[0];
              currCust.name = (string)reader[1];
              currCust.streetAddress = (string)reader[2];
              currCust.city = (string)reader[3];
              currCust.state = (string)reader[4];
              currCust.postalCode = (string)reader[5];
              currCust.phoneNumber = (string)reader[6];
              customerList.Add(currCust);
            }
          }
        }
      }
      return customerList;
    } // End getCustomers()

    public void addCustomer(Customer newCustomer)
    {
      SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True");
      SqlCommand cmd = new SqlCommand();
      StringBuilder command = new StringBuilder();
      command.Append("INSERT INTO Customer (Name, StreetAddress, City, ")
      .Append("StateProvince, PostalCode, PhoneNumber) VALUES (")
      .Append("'").Append(newCustomer.name).Append("', ")
      .Append("'").Append(newCustomer.streetAddress).Append("', ")
      .Append("'").Append(newCustomer.city).Append("', ")
      .Append("'").Append(newCustomer.state).Append("', ")
      .Append("'").Append(newCustomer.postalCode).Append("', ")
      .Append("'").Append(newCustomer.phoneNumber).Append("')");
      
      cmd.CommandType = System.Data.CommandType.Text;
      cmd.CommandText = command.ToString();
      cmd.Connection = sqlConnection1;

      sqlConnection1.Open();
      cmd.ExecuteNonQuery();
      sqlConnection1.Close();
    } // End addCustomer()

    public List<PaymentOption> getPmtOptions(int custId)
    {
      string query = ("SELECT * FROM PaymentOption WHERE IdCustomer = " + custId);

      List<PaymentOption> pmtOptions = new List<PaymentOption>();

      using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True"))
      using (SqlCommand cmd = new SqlCommand(query, connection))
      {
        connection.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read()) // Read advances to the next row.
            {
              PaymentOption currOption = new PaymentOption();
              currOption.idPaymentOption = (int)reader[0];
              currOption.idCustomer = (int)reader[1];
              currOption.name = (string)reader[2];
              currOption.accountNumber = (string)reader[3];
              pmtOptions.Add(currOption);
            }
          }
        }
      }
      return pmtOptions;
    } // End getPmtOptions()

    public void addPmtOption(PaymentOption newPmt)
    {
      SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True");
      SqlCommand cmd = new SqlCommand();
      StringBuilder command = new StringBuilder();
      command.Append("INSERT INTO PaymentOption (IdCustomer, Name, AccountNumber) VALUES (")
      .Append("'").Append(newPmt.idCustomer).Append("', ")
      .Append("'").Append(newPmt.name).Append("', ")
      .Append("'").Append(newPmt.accountNumber).Append("')");
      
      cmd.CommandType = System.Data.CommandType.Text;
      cmd.CommandText = command.ToString();
      cmd.Connection = sqlConnection1;

      sqlConnection1.Open();
      cmd.ExecuteNonQuery();
      sqlConnection1.Close();
    } // End addPmtOption()

    public void addOrder(int idCust, int idPmt, List<Product> orderProds)
    {
      // 1. Generate date for DateCreated, also use this to pull orderId
      DateTime orderDate = DateTime.Now;
      
      // 2. Add order to CustomerOrder Table
      SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True");
      SqlCommand cmd = new SqlCommand();
      StringBuilder command = new StringBuilder();
      command.Append("INSERT INTO CustomerOrder (DateCreated, IdCustomer, IdPaymentOption) VALUES (")
      .Append("'").Append(orderDate.Date.ToString("yyyy-MM-dd")).Append("', ")
      .Append("'").Append(idCust).Append("', ")
      .Append("'").Append(idPmt).Append("')");

      cmd.CommandType = System.Data.CommandType.Text;
      cmd.CommandText = command.ToString();
      cmd.Connection = sqlConnection1;

      sqlConnection1.Open();
      cmd.ExecuteNonQuery();
      sqlConnection1.Close();

      int idOrder = 0; // Get from order that was just added
      string query2 = ("SELECT MAX(IdOrder) AS maxID FROM CustomerOrder");

      using (SqlConnection connection2 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True"))
      using (SqlCommand cmd2 = new SqlCommand(query2, connection2))
      {
        connection2.Open();
        using (SqlDataReader reader = cmd2.ExecuteReader())
        {
          if (reader.Read())
          {
            idOrder = (int)reader[0];
            Console.WriteLine("idOrder: {0}", idOrder);
            System.Threading.Thread.Sleep(1000);
          }
        }
      }

      // 3. Loop through orderProds list and add each product to OrderProducts table
      foreach (Product prod in orderProds)
      {
        SqlConnection sqlConnection3 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True");
        SqlCommand cmd3 = new SqlCommand();
        StringBuilder command3 = new StringBuilder();
        command3.Append("INSERT INTO OrderProducts (IdProduct, IdOrder) VALUES (")
        .Append("'").Append(prod.idProduct).Append("', ")
        .Append("'").Append(idOrder).Append("')");

        cmd3.CommandType = System.Data.CommandType.Text;
        cmd3.CommandText = command3.ToString();
        cmd3.Connection = sqlConnection3;

        sqlConnection3.Open();
        cmd3.ExecuteNonQuery();
        sqlConnection3.Close();
      }
    }

  } // End InvoiceDb Class
} // End Namespace

