using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        Mock<ILogger<UsersController>> mock_logger = new Mock<ILogger<UsersController>>();
        Mock<IUserManager> mockUserManager = new Mock<IUserManager>();

        UsersController userController => new UsersController(mockUserManager.Object, mock_logger.Object);

        [Fact]
        public async void It_ShouldCreateUserWhenUserIsValid()
        {
            var user = new User()
            {
                Name = "Kenny",
                Email = "random@gmail.com",
                Address = "NoWhere",
                Phone = "123456789",
                UserType = "Normal",
                Money = 80,
            };

            var result = new Result() { Errors = null, IsSuccess = true };
            mockUserManager.Setup(x => x.CreateUser(user)).ReturnsAsync(result);
            var response = await userController.CreateUser(user) as ObjectResult;

            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async void It_ShouldCreateUserFromUserManager()
        {
            var user = new User()
            {
                Name = "Kenny",
                Email = "random@gmail.com",
                Address = "NoWhere",
                Phone = "123456789",
                UserType = "Normal",
                Money = 80,
            };

            await userController.CreateUser(user);

            mockUserManager.Verify(u => u.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void It_ShouldNotCreateUserWhenInputBodyIsInvalid()
        {
            var badbody = new User { Name = "JustHasName" };

            var actionResult = await userController.CreateUser(badbody);

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void It_ShouldNotCreateUserWhenUserIsInvalid()
        {
            var user = new User()
            {
                Name = "",
                Email = "random@gmail.com",
                Address = "NoWhere",
                Phone = "123456789",
                UserType = "Normal",
                Money = 80,
            };

            var result = new Result()
            {
                IsSuccess = false,
                Errors = "Validation error message"
            };

            var actionResult = await userController.CreateUser(user);

            Assert.IsType<BadRequestObjectResult>(actionResult);           
        }
    }
}
