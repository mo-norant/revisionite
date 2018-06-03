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
      private readonly UserManager<ApplicationUser> _usermanager;
      private readonly RoleManager<IdentityRole> _rolemanager;
      private readonly SignInManager<ApplicationUser> _signinmanager;
      private readonly ApplicationDbContext _context;
      private readonly IHostingEnvironment _env;


      public ProductController(
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

      [HttpPost]
      public async Task<IActionResult> Product( [FromBody] Product temp )
      {
        var user = await _usermanager.GetUserAsync(User);


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
            ProductCategories = temp.ProductCategories,
            Base64 = temp.Base64
       
          };
          product.UserID = user.Id;
          await _context.Products.AddAsync(product);
          await _context.SaveChangesAsync();
          return Ok(product.ProductID);
        }

        return BadRequest();
      }

      [HttpGet("count")]
      public async Task<IActionResult> ProductCount()
      {
        var user = await _usermanager.GetUserAsync(User);

        if(user != null)
        {

          var productscount = await _context.Products.Where(p => p.UserID == user.Id).CountAsync();

          return Ok(productscount);
        }

        return BadRequest();
      }

    [HttpGet]
    public async Task<IActionResult> Product([FromQuery] int index, [FromQuery] int count, [FromQuery] string orderby, [FromQuery] bool desc)
    {
      var user = await _usermanager.GetUserAsync(User);

      if (user == null) return BadRequest();
      var actualcount = await _context.Products.Where(p => p.UserID == user.Id).CountAsync();

      if (actualcount == 0)
      {
        return NotFound("No items");
      }

      if (desc)
      {
        switch (orderby)
        {
          case "ProductID":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderByDescending(i => i.ProductID).Skip(index * count).Take(count).ToListAsync());

          case "ProductTitle":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                  .OrderByDescending(i => i.ProductTitle).Skip(index * count).Take(count).ToListAsync());
          case "Description":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderByDescending(i => i.Description).Skip(index * count).Take(count).ToListAsync());
          case "Price":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderByDescending(i => i.Price).Skip(index * count).Take(count).ToListAsync());
          case "Date":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderByDescending(i => i.Create).Skip(index * count).Take(count).ToListAsync());
        }
        return null;



      }

      else
      {

        switch (orderby)
        {
          case "ProductID":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderBy(i => i.ProductID).Skip(index * count).Take(count).ToListAsync());

          case "ProductTitle":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderBy(i => i.ProductTitle).Skip(index * count).Take(count).ToListAsync());
          case "Description":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderBy(i => i.Description).Skip(index * count).Take(count).ToListAsync());
          case "Price":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderBy(i => i.Price).Skip(index * count).Take(count).ToListAsync());
          case "Date":
            return Ok(await _context.Products.Where(p => p.UserID == user.Id).Include(i => i.ProductCategories)
                      .OrderBy(i => i.Create).Skip(index * count).Take(count).ToListAsync());
        }
        return null;
      }
    }



      [HttpPost("update/{id}")]
      public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product temp)
      {
        var user = await _usermanager.GetUserAsync(User);

        var product = await _context.Products.Where(i => i.ProductID == id).Include(pr => pr.ProductCategories).Where(incoming => incoming.UserID == user.Id).FirstOrDefaultAsync();
        product.Description = temp.Description;
        product.ProductTitle = temp.ProductTitle;
        product.Price = temp.Price;
        await _context.SaveChangesAsync();
          

        return Ok();

      

      }



      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteProduct([FromRoute] int id)
      {
        var user = await _usermanager.GetUserAsync(User);

        if(user != null)
        {
          var item = await _context.Products.Where(p => p.ProductID == id).Where(i => i.UserID == user.Id).Include(i => i.ProductCategories).FirstOrDefaultAsync();

          _context.RemoveRange(item.ProductCategories);
          _context.Remove(item);
          await _context.SaveChangesAsync();
          return Ok();

        }

        return BadRequest();

      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetProduct([FromRoute] int id)
      {
        var user = await _usermanager.GetUserAsync(User);

        if(user != null)
        {
          var item = await _context.Products.Where(i => i.ProductID == id).Where(i => i.UserID == user.Id).Include(p => p.ProductCategories).FirstOrDefaultAsync();

          if(item == null)
          {
            return NotFound();
          }

          return Ok(item);

        }

        return BadRequest();
      }

      private void PrintToConsole(string log){
        Console.WriteLine(log);
      }

     }
    }
