using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangazon
{
  public class PopularProduct
  {
    public string name { get; set; }
    public int numOrdered { get; set; }
    public int numCustomers { get; set; }
    public double totalRevenue { get; set; }
  }
}
