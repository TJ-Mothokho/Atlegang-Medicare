using Atlegang_Medicare.Controllers.Administrator;
using Atlegang_Medicare.Controllers.Doctor;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlegang_Unit_Tests.Controllers.Administrator
{
    public class AdministratorControllerTests
    {
        private IAdministratorDashboardCardsRepository _administratorDashboardCardsRepository;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private ISuburbRepository _suburbRepository;
        private ICityRepository _cityRepository;
        private IWardRepository _wardRepository;
        private IBedRepository _bedRepository;
        private HttpContextAccessor _httpContextAccessor;

        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<ISuburbRepository> _suburbRepositoryMock;
        private Mock<ICityRepository> _cityRepositoryMock;
        private Mock<IWardRepository> _wardRepositoryMock;
        private Mock<IBedRepository> _bedRepositoryMock;



        private AdministratorController _controller;
        private AdministratorController _mockController;

        public AdministratorControllerTests()
        {
            //DI
            _administratorDashboardCardsRepository = A.Fake<IAdministratorDashboardCardsRepository>();
            _userRepository = A.Fake<IUserRepository>();    
            _roleRepository = A.Fake<IRoleRepository>();
            _suburbRepository = A.Fake<ISuburbRepository>();
            _cityRepository = A.Fake<ICityRepository>();
            _wardRepository = A.Fake<IWardRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            _bedRepository = A.Fake<IBedRepository>();


            _userRepositoryMock = new Mock<IUserRepository>();
            _wardRepositoryMock = new Mock<IWardRepository>();
            _bedRepositoryMock = new Mock<IBedRepository>();

            //SUT
            _controller = new AdministratorController(_bedRepository,_wardRepository, _userRepository, _roleRepository, _suburbRepository, _cityRepository, _administratorDashboardCardsRepository);
            _mockController = new AdministratorController(_bedRepositoryMock.Object,_wardRepositoryMock.Object, _userRepositoryMock.Object, _roleRepository, _suburbRepository, _cityRepository, _administratorDashboardCardsRepository);
        }
        [Fact]
        public void ListBeds_Returns_ViewResult()
        {
            //Arrange
            var beds = A.Fake<IEnumerable<Bed>>();
            A.CallTo(() => _bedRepository.GetAllBedsAsync()).Returns(beds);

            //Act
            var result = _controller.ListBeds();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void AddBed_Post_Returns_ViewResult()
        {
            // Arrange
            var wardName = new Ward
            {
                WardID = 1               
            };

            // Act
            var result = _mockController.AddBed(wardName);

            // Assert
            _bedRepositoryMock.Verify(repo => repo.AddBedAsync(wardName.WardID), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            result.Should().NotBeNull();
        }
        [Fact]
        public void DeleteBed_Post_Returns_ViewResult()
        {
            // Arrange
            var bed = new Bed
            {
                BedID = 1,
                RoomID = 4,              
                Status = "Active"
            };

            // Act
            var result = _mockController.DeleteBed(bed);

            // Assert
            _bedRepositoryMock.Verify(repo => repo.DeleteBedAsync(bed.BedID), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            result.Should().NotBeNull();
        }
       
        [Fact]
        public void AddEmployee_Post_Returns_ViewResult()
        {
            // Arrange
            var employee = new User
            {
                UserID = 1,
                FirstName = "Brilliant",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Password = "Brilliant@1234_",
                PhoneNumbers = "0813452365",
                Email = "brilliant@example.com",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Active"
            };

            // Act
            var result = _mockController.AddEmployee(employee);

            // Assert
            _userRepositoryMock.Verify(repo => repo.AddEmployeeAsync(employee), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            employee.Should().NotBeNull();
        }

        [Fact]
        public void ListEmployee_Returns_ViewResult()
        {
            //Arrange
            var employees = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _userRepository.GetAllEmployeesAsync()).Returns(employees);

            //Act
            var result = _controller.ListEmployee();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }


        [Fact]
        public void ViewEmployee_Returns_ViewResultID()
        {
            //Arrange
            var employeeById = A.Fake<User>();
            A.CallTo(() => _userRepository.GetUserByIdAsync(1)).Returns(employeeById);

            //Act
            var result = _controller.ViewEmployee(1);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void UpdateEmployee_Post_Returns_ViewResult()
        {
            // Arrange
            var employee = new User
            {
                UserID = 1,
                FirstName = "Brilliant",
                LastName = "Motjiang",
                Gender = "Male",
                Password = "Brilliant@1234_",
                PhoneNumbers = "0813452365",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Active"
            };

            // Act
            var result = _mockController.UpdateEmployee(employee);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateEmployeeAsync(employee), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            employee.Should().NotBeNull();
        }

        [Fact]
        public void DeleteEmployee_Post_Returns_ViewResult()
        {
            // Arrange
            var employee = new User
            {
                UserID = 1,
                FirstName = "Brilliant",
                LastName = "Motjiang",
                Gender = "Male",
                Password = "Brilliant@1234_",
                PhoneNumbers = "0813452365",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "InActive"
            };
            // Act
            var result = _mockController.DeleteEmployee(employee);

            // Assert
            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(employee.UserID), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            employee.Should().NotBeNull();
        }
    }
}
