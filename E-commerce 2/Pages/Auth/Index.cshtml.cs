using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.DTO;
using E_commerce_2.Auth.Models.Interface;
using E_commerce_2.Auth.Models.Services;
using IdentityServer3.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_commerce_2.Pages.Auth
{
    public class IndexModel : PageModel
    {

        public readonly IUser _user;

        public IndexModel(IUser user)
        {
            _user = user;
        }

        [BindProperty]
        public RegisterUserDTO registerUser { get; set; }
        public UserDTO user { get; set; }

        public List<ApplicationUser> applicationUsers { get; set; }

        public LoginDTO loginDTO { get; set; }

        public async Task<IActionResult> OnPostAsync(LoginDTO loginDTO)
        {

            user = await _user.Authenticate(loginDTO.Username, loginDTO.Password);

            if (user != null)
            {
                return RedirectToPage("Home");
            }
            else
            {
                ViewData["WrongUser"] = "user name or password is wrong ";

                return null;
            }


        }

        public void OnGet()
        {
        }
    }
}
