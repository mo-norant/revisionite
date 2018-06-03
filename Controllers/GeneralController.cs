using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularSPAWebAPI.Data;
using AngularSPAWebAPI.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularSPAWebAPI.Controllers
{
  [Route("api/[controller]")]
  [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = "Access Resources")]
  public class GeneralController : Controller
    {
    private readonly UserManager<ApplicationUser> _usermanager;
    private readonly RoleManager<IdentityRole> _rolemanager;
    private readonly SignInManager<ApplicationUser> _signinmanager;
    private readonly ApplicationDbContext _context;
    private readonly IHostingEnvironment _env;


    public GeneralController(
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



    [HttpPost("validtoken")]
    public IActionResult ValidToken(){


      var isSignedIn = _signinmanager.IsSignedIn(User);
      return Ok(isSignedIn);


    }
    [HttpPost("SignOut")]
    public async Task<IActionResult> SignOut()
    {

      await _signinmanager.SignOutAsync();
      return Ok();


    }

    [HttpGet("email")]
    public async Task<IActionResult> GetEmail()
    {
      var user = await _usermanager.GetUserAsync(User);

      if (user != null)
      {


        return Json(user.Email);
      }

      return BadRequest();
    }
  }
}
