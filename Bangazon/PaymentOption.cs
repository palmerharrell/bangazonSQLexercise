using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bangazon
{
  public class PaymentOption
  {
    public int idPaymentOption { get; set; }
    public int idCustomer { get; set; }
    public string name { get; set; }
    public string accountNumber { get; set; }

    public static List<PaymentOption> getPmtOptions(int custId)
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

    public static void addPmtOption(PaymentOption newPmt)
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

  }
}

