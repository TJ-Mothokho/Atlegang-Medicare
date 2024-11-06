using Atlegang_Medicare.Controllers.Doctor;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Repository.Doctor;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlegang_Unit_Tests.Controllers.Doctor
{
    public class EventVisitControllerTests
    {
        private  IPatientFileRepository _patientFileRepository; // Interface for accessing patient file data.
        private  IVisitRepository _visitRepository; // Interface for accessing visit data.
        private Mock<IVisitRepository> _visitRepositoryMock;


        private EventVisitController _controller;
        private EventVisitController _mockController;

        public EventVisitControllerTests()
        {
            //DI
            _patientFileRepository = A.Fake<IPatientFileRepository>();
            _visitRepository = A.Fake<IVisitRepository>();
            _visitRepositoryMock = new Mock<IVisitRepository>();

            //SUT
            _controller = new EventVisitController(_patientFileRepository, _visitRepository);

            _mockController = new EventVisitController( _patientFileRepository, _visitRepositoryMock.Object);

        }
        [Fact]
        public void AddVisit_Post_Returns_ViewResult()
        {
            // Arrange
            var visit = new Visit
            {
                VisitID = 1,
                FileID = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Title = "Initial Visit",
                Description = "Consultation",
                DoctorID = 14,                
            };

            // Act
            var result = _mockController.AddVisit(visit);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

            visit.Should().NotBeNull();
        }

        [Fact]
        public void ListVisit_Returns_ViewResult()
        {
            //Arrange
            var visitList = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _visitRepository.GetAllVisitsAsync()).Returns(visitList);

            //Act
            var result = _controller.ListVisit();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ViewVisit_Returns_ViewResultID()
        {
            //Arrange
            var visit = A.Fake<VisitViewModel>();
            A.CallTo(() => _visitRepository.GetVisitByIdAsync(1)).Returns(visit);

            //Act
            var result = _controller.ViewVisit(1);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void UpdateVisit_Post_Returns_ViewResult()
        {
            // Arrange
            var visit = new Visit
            {
                VisitID = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Title = "Initial Visit",
                Description = "Consultation"
            };

            // Act
            var result = _mockController.UpdateVisit(visit);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();

            visit.Should().NotBeNull();
        }
    }
}
