using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_commerceTestData
{
    public class PasswordValidator
    {
        public bool IsValid(string password)
        {
            Regex passwordPolicyExpression = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#!$%]).{8,20})");
            return passwordPolicyExpression.IsMatch(password);
        }
    }
    public class LoginTest
    {
        [Fact]
        public void ValidPassword()
        {
            //Arrange
            var passwordValidator = new PasswordValidator();
            const string password = "Th1sIsapassword!";

            //Act
            bool isValid = passwordValidator.IsValid(password);

            //Assert
            Assert.True(isValid, $"The password {password} is not valid");
        }
        [Fact]
        public void NotValidPassword()
        {
            //Arrange
            var passwordValidator = new PasswordValidator();
            const string password = "thisIsaPassword";

            //Act
            bool isValid = passwordValidator.IsValid(password);

            //Assert
            Assert.False(isValid, $"The password {password} should not be valid!");
        }
    }
}
