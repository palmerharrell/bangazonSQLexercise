using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class Order
  {
    public int idOrder { get; }
    public DateTime dateCreated = new DateTime();
    public int idCustomer { get; set; }
    public int idPaymentOption { get; set; }
    public string shippingMethod { get; set; }

    public static void addOrder(int idCust, int idPmt, List<Product> orderProds)
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
            idOrder = (int)reader["maxID"];
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
    }// End addOrder()

  }
}
