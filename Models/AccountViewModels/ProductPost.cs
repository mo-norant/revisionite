using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSPAWebAPI.Models.AccountViewModels
{
    public class ProductPost
    {
    public string ProductTitle { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
  }
}
