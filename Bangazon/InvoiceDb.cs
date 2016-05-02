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
    }

  }
}
