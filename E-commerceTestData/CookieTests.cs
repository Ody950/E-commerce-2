using System;
using System.Threading.Tasks;
using E_commerce_2.Auth.Models.DTO;
using E_commerce_2.Auth.Models.Interface;
using E_commerce_2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Xunit;

namespace E_commerce_2.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IUser> _userMock;

        public AuthControllerTests()
        {
            _userMock = new Mock<IUser>();
            _controller = new AuthController(_userMock.Object);
        }

        [Fact]
        public async Task SignUpReturnsUserDTOWhenModelStateIsValid()
        {
            // Arrange
            var registerDTO = new RegisterUserDTO
            {
            };
            var modelState = new ModelStateDictionary();

            // Act
            var result = await _controller.SignUp(registerDTO, modelState) as ActionResult<UserDTO>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result.Value);
        }

        [Fact]
        public async Task SignUp_ReturnsUserDTO_WhenModelStateIsValid()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var controller = new AuthController(userMock.Object);
            var registerDTO = new RegisterUserDTO
            {
            };
            var modelState = new ModelStateDictionary();


            // Act
            var result = await controller.SignUp(registerDTO, modelState);

            // Assert
            Assert.NotNull(result);

            if (result is ActionResult<UserDTO> actionResult && actionResult.Value is UserDTO userDto)
            {
                Assert.IsType<UserDTO>(userDto);
            }
            else
            {
                Assert.True(false, $"Unexpected result type: {result.GetType()}");
            }
        }


    }
}
