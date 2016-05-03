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
      // 1. Add order to CustomerOrder Table
      // 2. Get IdOrder for order that was just added
      // 3. Loop through orderProds list and add each product to OrderProducts table
    }

  } // End InvoiceDb Class
} // End Namespace

