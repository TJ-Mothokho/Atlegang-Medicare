using Moq;
using DataAccesslayer.Repository.Administrator;
using Atlegang_Medicare.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Models.Domains.Administrator;
using Microsoft.AspNetCore.Http;
using FluentAssertions;
using DataAccesslayer.Services;


namespace Atlegang_Unit_Tests.Controllers.Account
{
    public class AccountControllerTests
    {
        //private readonly Mock<IUserRepository> _userRepo;
        //private readonly Mock<ISuburbRepository> _suburbRepo;
        //private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        //private readonly AccountController _controller;

        //public AccountControllerTests()
        //{
        //    _userRepo = new Mock<IUserRepository>();
        //    _suburbRepo = new Mock<ISuburbRepository>();
        //    _webHostEnvironment = new Mock<IWebHostEnvironment>();
        //    _controller = new AccountController(_userRepo.Object, _suburbRepo.Object, _webHostEnvironment.Object);
        //}

        //[Fact]
        //public void LoginPage_Returns_ViewResult()
        //{
        //    // Act
        //    var result = _controller.LoginPage();

        //    // Assert
        //    result.Should().BeOfType<ViewResult>()
        //        .Which.ViewName.Should().BeNull(); // Asserting that the default view is returned
        //}


        //[Fact]
        //public async Task LoginPage_Post_ValidCredentials_RedirectsToHome()
        //{
        //    // Arrange

        //    var user = new User { UserID = 1, RoleID = 2, Email = "user@example.com", Password = "validPassword" };

        //    // Create an instance of the PasswordHasher
        //    PasswordHasher handler = new PasswordHasher();

        //    // Hash the password in the same way the controller does
        //    string hashedPassword = handler.HashPassword(user.Password);

        //    // Setup the repository mock to return the user when the hashed password is used
        //    _userRepo
        //        .Setup(repo => repo.Login(user.Email, hashedPassword))
        //        .ReturnsAsync(user);

        //    // Act
        //    var result = await _controller.LoginPage(user.Email, user.Password);

        //    // Assert
        //    _userRepo.Verify(repo => repo.Login(user.Email, hashedPassword), Times.Once);
        //}



    }
}
