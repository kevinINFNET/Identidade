using Identidade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identidade.Pages.Account
{
public class RegisterModel : PageModel
 {
[BindProperty]
 public string Username { get; set; }
[BindProperty]
  public string Password { get; set; }
 public IActionResult OnPost()
  {
  if (UserStorage.Users.Exists(u => u.Username == Username))
 {
  ModelState.AddModelError(string.Empty, "Usuário já existe.");
return Page();
 }
 UserStorage.Users.Add(new User { Username = Username, Password = Password });
  return RedirectToPage("/Account/Login"); 
   }
}
