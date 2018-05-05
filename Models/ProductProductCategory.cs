using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSPAWebAPI.Models
{
    public class ProductProductCategory
    {
    public int ProductID { get; set; }
    public int ProductCategoryID { get; set; }
    public Product Product { get; set; }
    public ProductCategory ProductCategory { get; set; }


  }
}
