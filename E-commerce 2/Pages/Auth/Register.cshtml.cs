using E_commerce_2.Auth.Models.DTO;
using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace E_commerce_2.Pages.Auth
{
    public class RegisterModel : PageModel
    {

        public readonly IUser _user;

        public RegisterModel(IUser user)
        {
            _user = user;
        }

        [BindProperty]
        public RegisterUserDTO registerUser { get; set; }
        public UserDTO user { get; set; }

        public List<ApplicationUser> applicationUsers { get; set; }

        public async Task OnGet()
        {

            applicationUsers = await _user.getAll();

        }


        public async Task<IActionResult> OnPostAsync(RegisterUserDTO registerUser)
        {

            user = await _user.Register(registerUser, this.ModelState);

            await OnGet();

            if (user != null)
            {
                return RedirectToPage("Home");
            }
            else
            {
                ViewData["WrongUser"] = "Some thing wrong ";

                return null;
            }
        }
    }
 
}
