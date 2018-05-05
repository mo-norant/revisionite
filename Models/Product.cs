using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSPAWebAPI.Models
{
    public class Product
    {
    public int ProductID { get; set; }
    public string UserID { get; set; }
    public string ProductTitle { get; set; }
    public string Description { get; set; }
    public int Views { get; set; }
    public float Price { get; set; }
    public DateTime Create { get; set; }
    public DateTime EndTime { get; set; }
    public string URI { get; set; }
    public virtual ICollection<ProductProductCategory> ProductProductCategories { get; set; }
  }
}
