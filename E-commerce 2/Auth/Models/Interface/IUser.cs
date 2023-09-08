using E_commerce_2.Auth.Models;
using E_commerce_2.Auth.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace E_commerce_2.Auth.Models.Interface

{
    public interface IUser
    {
        //Register Method
        public Task<UserDTO> Register(RegisterUserDTO registerDto, ModelStateDictionary modelstate);
        //login Method

        public Task<UserDTO> Authenticate(string username, string password);
        // Get All users method
        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
        // logout method
        public Task LogOut();
        public Task<List<ApplicationUser>> getAll();
    }
}