using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AngularSPAWebAPI.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AngularSPAWebAPI.Models;
using AngularSPAWebAPI.Models.AccountViewModels;
using Microsoft.AspNetCore.Cors;

namespace AngularSPAWebAPI.Controllers
{
  [Route("api/[controller]")]
  [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access Resources")]
  public class ProductController : Controller
    {
    private readonly UserManager<ApplicationUser> usermanager;
    private readonly RoleManager<IdentityRole> rolemanager;
    private readonly SignInManager<ApplicationUser> signinmanager;
    private readonly ApplicationDbContext context;
    private readonly IHostingEnvironment env;


    public ProductController(
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

    [HttpPost]
    public async Task<IActionResult> Product( [FromBody] Product temp )
    {
      var user = await usermanager.GetUserAsync(User);


      if(user != null)
      {

        foreach (var pc in temp.ProductCategories)
        {
          pc.Date = DateTime.Now;
        }

        var product = new Product
        {
          UserID = user.Id,
          Create = DateTime.Now,
          Description = temp.Description,
          EndTime = temp.EndTime,
          ProductTitle = temp.ProductTitle,
          Price = temp.Price,
          ProductCategories = temp.ProductCategories
          
        };
        product.UserID = user.Id;
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return Ok(product.ProductID);
      }

      return BadRequest();
    }

    [HttpGet("count")]
    public async Task<IActionResult> ProductCount()
    {
      var user = await usermanager.GetUserAsync(User);

      if(user != null)
      {

        var productscount = await context.Products.Where(p => p.UserID == user.Id).CountAsync();

        return Ok(productscount);
      }

      return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Product([FromQuery] int index, [FromQuery] int count )
    {
      var user = await usermanager.GetUserAsync(User);

      if(user != null)
      {
        var actualcount = await context.Products.Where(p => p.UserID == user.Id).CountAsync();

        if (actualcount == 0)
        {
          return NotFound("No items");
        }

        if (actualcount > index * count)
        {
          var products = await context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
            .Skip(index * count).Take(count).ToListAsync();

          return Ok(products);

        }

        else
        {

          var tempindex = actualcount - count;

          if(tempindex < 0)
          {
            count = actualcount;
            tempindex = 0;
          }

          var products = await context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories).Skip(tempindex).Take(count).ToListAsync();

          return Ok(products);
        }









      }

      return BadRequest();

    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product, [FromRoute] int ID)
    {
      var user = await usermanager.GetUserAsync(User);

      if (user != null)
      {
        var old = await context.Products.Where(i => product.ProductID == product.ProductID).Where(i => i.UserID == product.UserID).FirstOrDefaultAsync();
        if (old == null)
        {
          return NotFound();
        }


        context.Entry(old).CurrentValues.SetValues(product);

        foreach (var pc in old.ProductCategories.ToList())
        {
          if (!product.ProductCategories.Any(c => c.ProductCategoryID == pc.ProductCategoryID))
            context.ProductCategories.Remove(pc);
        }

        foreach (var pc in product.ProductCategories)
        {
          var existingChild = old.ProductCategories
              .Where(c => c.ProductCategoryID == pc.ProductCategoryID)
              .SingleOrDefault();

          if (existingChild != null)
            // Update child
            context.Entry(existingChild).CurrentValues.SetValues(pc);
          else
          {
            // Insert child
            var productcat = new ProductCategory
            {
              ProductCategoryTitle = pc.ProductCategoryTitle,
              Date = DateTime.Now
            };
            old.ProductCategories.Add(productcat);
          }
        }
        await context.SaveChangesAsync();
        return Ok();

      }

      return BadRequest();
    }


    [HttpPost("File/{id}")]
    public async Task<IActionResult> Product([FromRoute] int Id)
    {
      var user = await usermanager.GetUserAsync(User);

      if (user != null)
      {

        var product = await context.Products.Where(i => i.UserID == user.Id).Where(i => i.ProductID == Id).FirstOrDefaultAsync();

        if (product == null)
        {
          return NotFound();
        }

        var files = HttpContext.Request.Form.Files;

        foreach (var image in files)
        {
          if (image != null && image.Length > 0)
          {
            var file = image;
            var uploads = Path.Combine(env.WebRootPath, "uploads\\image");
            if (file.Length > 0)
            {
              var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
              using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
              {
                await file.CopyToAsync(fileStream);
                product.URI = fileName;
              }
            }
          }
        }
        await context.SaveChangesAsync();
        return Ok();
      }

      return BadRequest();

      }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
      var user = await usermanager.GetUserAsync(User);

      if(user != null)
      {
        var item = await context.Products.Where(p => p.ProductID == id).Where(i => i.UserID == user.Id).Include(i => i.ProductCategories).FirstOrDefaultAsync();

        context.RemoveRange(item.ProductCategories);
        context.Remove(item);
        await context.SaveChangesAsync();
        return Ok();

      }

      return BadRequest();

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
      var user = await usermanager.GetUserAsync(User);

      if(user != null)
      {
        var item = await context.Products.Where(i => i.ProductID == id).Where(i => i.UserID == user.Id).Include(p => p.ProductCategories).FirstOrDefaultAsync();

        if(item == null)
        {
          return NotFound();
        }

        return Ok(item);

      }

      return BadRequest();
    }

   }
  }
