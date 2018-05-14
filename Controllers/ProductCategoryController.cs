
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

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
      var user = await usermanager.GetUserAsync(User);

      if (user != null)
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

        return Ok(pcs.Count);
      }

      return BadRequest();

    }

    [HttpGet]
    public async Task<IActionResult> GetProductCategories([FromQuery] int index, [FromQuery] int count)
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

        return Ok(pcs.Skip(index*count).Take(count));
      }

      return BadRequest();

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateProductCategory([FromRoute] int id, [FromQuery] string newtitle)
    {

      var item = await context.ProductCategories.Where(i => i.ProductCategoryID == id).FirstOrDefaultAsync();

      if(item != null)
      {
        item.ProductCategoryTitle = newtitle;
        await context.SaveChangesAsync();
        return Ok();
      }

      return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] int id)
    {
      var pc = await context.ProductCategories.Where(i => i.ProductCategoryID == id).FirstOrDefaultAsync();
      if(pc !=null)
      {
        context.Remove(pc);
        await context.SaveChangesAsync();
        return Ok();
      }

      return BadRequest();
    }

  }
}
