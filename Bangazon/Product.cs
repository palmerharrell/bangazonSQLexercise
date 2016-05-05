using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class Product
  {
    public int idProduct { get; set; }
    public int idProductType { get; set; }
    public string name { get; set; }
    public float price { get; set; }
    public string description { get; set; }

    public static List<Product> getProducts()
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

    public static List<PopularProduct> getPopularProducts()
    {
      string query = @"
        SELECT Product.Name, 
        COUNT(OrderProducts.IdProduct) AS [numOrdered], 
        COUNT(DISTINCT CustomerOrder.IdCustomer) AS [numCustomers], 
        ROUND(SUM(Product.Price),2) AS [totalRevenue]
        FROM Product
        INNER JOIN OrderProducts on OrderProducts.IdProduct = Product.IdProduct
        INNER JOIN CustomerOrder on CustomerOrder.IdOrder = OrderProducts.IdOrder
        GROUP BY Product.Name
      ";

      List<PopularProduct> popProds = new List<PopularProduct>();

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
              PopularProduct popProd = new PopularProduct();
              popProd.name = (string)reader[0];
              popProd.numOrdered = (int)reader[1];
              popProd.numCustomers = (int)reader[2];
              popProd.totalRevenue = (double)reader[3];
              popProds.Add(popProd);
            }
          }
        }
      }
      return popProds;
    } // End getPopularProducts()

  }
}
