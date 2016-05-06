using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bangazon
{
  public class Customer
  {
    public int idCustomer { get; set; }
    public string name { get; set; }
    public string streetAddress { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string postalCode { get; set; }
    public string phoneNumber { get; set; }

    public static List<Customer> getCustomers()
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

    public static void addCustomer(Customer newCustomer)
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
