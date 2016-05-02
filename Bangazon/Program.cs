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
      // SQL CONNECTION AND READ TEST
      string query = @"
        SELECT
          co.DateCreated AS [Date],
          c.FirstName AS [First Name],
          c.LastName AS [Last Name],
          c.FirstName + ' ' + c.LastName AS [Full Name],
          co.OrderNumber AS [Order #],
          po.Name AS [Payment Type],
          p.Name AS [Product Name],
          pt.Name AS [Type]
        FROM CustomerOrder co
        INNER JOIN OrderProducts op ON op.IdOrder = co.IdOrder
        INNER JOIN Product p ON p.IdProduct = op.IdProduct
        INNER JOIN Customer c ON c.IdCustomer = co.IdCustomer
        INNER JOIN ProductType pt ON pt.IdProductType = p.IdProductType
        INNER JOIN PaymentOption po ON po.IdPaymentOption = co.IdPaymentOption
        ";

      string query2 = @"
        SELECT * FROM Product
      ";

      using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\windows-workspace\\bangazonCLIordering\\Bangazon\\Invoices.mdf\";Integrated Security=True"))
      using (SqlCommand cmd = new SqlCommand(query2, connection))
      {
        connection.Open();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          // Check if the reader has any rows at all before starting to read.
          if (reader.HasRows)
          {
            // Read advances to the next row.
            while (reader.Read())
            {
              Console.WriteLine("{0}, {1}, {2}, {3}",
                  reader[0], reader[1], reader[2], reader[3]);
            }

          }
        }
      } // using (sqlCommand...
    } // Main
  } // Program Class
} // Namespace
