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
using AngularSPAWebAPI.Models;
using AngularSPAWebAPI.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AngularSPAWebAPI.Models.AccountViewModels;

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
    public async Task<IActionResult> Product( [FromBody] ProductPost temp )
    {
      var user = await usermanager.GetUserAsync(User);

      var jointemps = new List<ProductProductCategory>();

      if(user != null)
      {

        var product = new Product
        {
          UserID = user.Id,
          Create = DateTime.Now,
          Description = temp.Description,
          EndTime = temp.EndTime,
          ProductTitle = temp.ProductTitle,
          Price = temp.Price

        };
        product.UserID = user.Id;
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        foreach (var productcattemp in temp.ProductCategories)
        {
          var productcat = new ProductCategory
          {
            Date = DateTime.Now,
            ProductCategoryTitle = productcattemp.ProductCategoryTitle
            
          };

          await context.ProductCategories.AddAsync(productcat);
          await context.SaveChangesAsync();

          var ppc = new ProductProductCategory
          {
            Product = product,
            ProductCategory = productcat
          };

          jointemps.Add(ppc);
        }
        await context.AddRangeAsync(jointemps);
        await context.SaveChangesAsync();
        return Ok(product);
      }

      return BadRequest();
    }

    [HttpPost("File/{id}")]
    public async Task<IActionResult> Product([FromRoute] int Id)
    {
      var user = await usermanager.GetUserAsync(User);

      if (user != null)
      {

        var product = await context.Products.Where(p => p.ProductID == Id).Where(p => p.UserID == user.Id).FirstOrDefaultAsync();

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

    }
  }
