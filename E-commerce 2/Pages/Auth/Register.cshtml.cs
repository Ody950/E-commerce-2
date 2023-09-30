using E_commerce_2.Auth.Models.DTO;
using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using E_commerce_2.Models.Interface;

namespace E_commerce_2.Pages.Auth
{
    public class RegisterModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmail _email;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager; 
        public readonly IUser _user;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmail emailSender, IConfiguration configuration, IUser user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _email = emailSender;
            _configuration = configuration;
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
                var applicationUser = await _userManager.FindByEmailAsync(registerUser.Email);
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                string subject = "welcome email";
                string message =
                    $"Hello {applicationUser.UserName}," +
                    $"We are thrilled to extend a warm and hearty welcome to you at Japanese Shop, your new destination for a delightful Japanese shopping experience.\r\n" +
                    "Click here to shop more: https://e-commerce2.azurewebsites.net/";

                await _email.SendEmailAsync(applicationUser.Email, subject, message);

                return RedirectToPage("/Home"); 
            }
            else
            {
                ViewData["WrongUser"] = "Some thing wrong ";

                return null;
            }

           
        }


    }

}
