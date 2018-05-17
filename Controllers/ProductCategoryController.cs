
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
    
    private readonly UserManager<ApplicationUser> _usermanager;
    private readonly RoleManager<IdentityRole> _rolemanager;
    private readonly SignInManager<ApplicationUser> _signinmanager;
    private readonly ApplicationDbContext _context;
    private readonly IHostingEnvironment _env;


    public ProductCategoryController(
        UserManager<ApplicationUser> usermanager,
        RoleManager<IdentityRole> rolemanager,
        SignInManager<ApplicationUser> signinmanager,
        ApplicationDbContext context, IHostingEnvironment env
            )
    {
      this._usermanager = usermanager;
      this._rolemanager = rolemanager;
      this._signinmanager = signinmanager;
      this._context = context;
      this._env = env;
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
      var user = await _usermanager.GetUserAsync(User);

      if (user == null) return BadRequest();
      var products = await _context.Products.Where(i => i.UserID == user.Id).Include(i => i.ProductCategories).ToListAsync();
      var pcs = new List<ProductCategory>();
      foreach (var pro in products)
      {
        pcs.AddRange(pro.ProductCategories);
      }

      return Ok(pcs.Count);

    }

    [HttpGet]
    public async Task<IActionResult> GetProductCategories([FromQuery] int index, [FromQuery] int count)
    {
      var user = await _usermanager.GetUserAsync(User);

      if (user == null) return BadRequest();
      var products = await _context.Products.Where(i => i.UserID == user.Id).Include(i => i.ProductCategories).ToListAsync();
      var pcs = new List<ProductCategory>();
      foreach (var pro in products)
      {
        pcs.AddRange(pro.ProductCategories);
      }

      return Ok(pcs.Skip(index*count).Take(count));

    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateProductCategory([FromRoute] int id, [FromQuery] string newtitle)
    {

      var item = await _context.ProductCategories.Where(i => i.ProductCategoryID == id).FirstOrDefaultAsync();

      if (item == null) return BadRequest();
      item.ProductCategoryTitle = newtitle;
      await _context.SaveChangesAsync();
      return Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] int id)
    {
      var pc = await _context.ProductCategories.Where(i => i.ProductCategoryID == id).FirstOrDefaultAsync();
      if (pc == null) return BadRequest();
      _context.Remove(pc);
      await _context.SaveChangesAsync();
      return Ok();

    }

  }
}
