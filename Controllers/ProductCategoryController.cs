
using System.Linq;
using System.Threading.Tasks;
using AngularSPAWebAPI.Data;
using AngularSPAWebAPI.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AngularSPAWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductCategory")]
  [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access Resources")]

  public class ProductCategoryController : Controller
  {
    
    private readonly UserManager<ApplicationUser> usermanager;
    private readonly RoleManager<IdentityRole> rolemanager;
    private readonly SignInManager<ApplicationUser> signinmanager;
    private readonly ApplicationDbContext context;
    private readonly IHostingEnvironment env;


    public ProductCategoryController(
        UserManager<ApplicationUser> usermanager,
        RoleManager<IdentityRole> rolemanager,
        SignInManager<ApplicationUser> signinmanager,
        ApplicationDbContext context, IHostingEnvironment env
            )
    {
      this.usermanager = usermanager;
      this.rolemanager = rolemanager;
      this.signinmanager = signinmanager;
      this.context = context;
      this.env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductCategories()
    {
      var user = await usermanager.GetUserAsync(User);

      if(user != null)
      {
        var products = await context.Products.Where(i => i.UserID == user.Id).Include(i => i.ProductCategories).ToListAsync();
        List<ProductCategory> pcs = new List<ProductCategory>();
        foreach (var pro in products)
        {
          foreach (var pc in pro.ProductCategories)
          {
            pcs.Add(pc);
          }
        }

        return Ok(pcs);
      }

      return BadRequest();

    }

  }
}
