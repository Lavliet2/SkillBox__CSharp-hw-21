using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebAPIContacts.AuthContactApp;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin model)
    {
        var user = await _userManager.FindByNameAsync(model.LoginProp);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _signInManager.SignInAsync(user, isPersistent: true);

            var sessionToken = Guid.NewGuid().ToString();

            return Ok(new { Token = sessionToken });
        }
        return Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistration model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User { UserName = model.LoginProp };        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var roleAddResult = await _userManager.AddToRoleAsync(user, model.Role);
            if (roleAddResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            else
            {
                foreach (var error in roleAddResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }

        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
