using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSPAWebAPI.Models
{
    public class ProductProductCategory
    {
    public int ProductID { get; set; }
    public int ProductCategoryID { get; set; }
    public virtual Product Product { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }


  }
}
