using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSPAWebAPI.Models
{
    public class ProductCategory
    {
    public int ProductCategoryID { get; set; }
    public string ProductCategoryTitle { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Product> Products { get; set; }
  }
}
