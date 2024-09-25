using Identidade.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Identidade.Pages.Account
{
public class LoginModel : PageModel
{
[BindProperty]
public string Username { get; set; }
[BindProperty]
public string Password { get; set; }
 public async Task<IActionResult> OnPostAsync()
{
 var user = UserStorage.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

if (user != null) 
{
 var claims = new List<Claim>
   {
 new Claim(ClaimTypes.Name, user.Username)
 };
 var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
 var authProperties = new AuthenticationProperties
{
  IsPersistent = true,
  ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
 };
await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
new ClaimsPrincipal(claimsIdentity),
 authProperties);
return LocalRedirect("/"); 
            }
ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
 return Page();
        }
    }
}
