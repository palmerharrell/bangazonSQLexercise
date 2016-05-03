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
    public bool parseInput(string userInput) // call methods from here? or set flags and call from Program?
    {
      return false;
    }

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
    }

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
  }
}
