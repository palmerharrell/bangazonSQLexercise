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
      
      string query2 = @"
        SELECT * FROM Product
        ORDER BY Product.Name
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
            Console.WriteLine(reader.GetName(2));
            Console.WriteLine(reader.HasRows);
            while (reader.Read())
            {
               Console.WriteLine("{0}, {1}, {2}, {3}",
                   reader[0], reader[1], reader[2], reader[3]);
              //Console.WriteLine("Field count: " + reader.FieldCount);
            }
          }
        }

      } // using (sqlCommand...
    } // Main
  } // Program Class
} // Namespace
