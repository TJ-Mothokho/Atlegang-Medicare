using Atlegang_Medicare.Controllers.Doctor;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using DataAccesslayer.Repository.Ward_Aministrator;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;

namespace Atlegang_Unit_Tests.Controllers.Doctor
{
    public class DoctorControllerTests
    {
        private IDoctorDashboardCardsRepository _doctorDashboardCardsRepository;
        private IPatientRepository _patientRepository;
        private IRoleRepository _roleRepository;
        private ISuburbRepository _suburbRepository;
        private IPatientFileRepository _patientFileRepository;
        private INoteRepository _noteRepository;
        private IMedicationRepository _medicationRepository;
        private IScriptRepository _scriptRepository;
        private IScriptDetailRepository _scriptDetailRepository;
        private IPatientTreatmentRepository _patientTreatmentRepository;
        private HttpContextAccessor _httpContextAccessor;

        private Mock<IPatientRepository> _patientRepositoryMock;
        private Mock<INoteRepository> _noteRepositoryMock;
        private Mock<IPatientFileRepository> _patientFileRepositoryMock;


        private DoctorController _controller;
        private DoctorController _mockController;

        public DoctorControllerTests()
        {
            //Dependances
            _doctorDashboardCardsRepository = A.Fake<IDoctorDashboardCardsRepository>();
            _patientRepository = A.Fake<IPatientRepository>();
            _suburbRepository = A.Fake<ISuburbRepository>();
            _patientFileRepository = A.Fake<IPatientFileRepository>();
            _noteRepository = A.Fake<INoteRepository>();
            _medicationRepository = A.Fake<IMedicationRepository>();
            _scriptRepository = A.Fake<IScriptRepository>();
            _scriptDetailRepository = A.Fake<IScriptDetailRepository>();
            _patientTreatmentRepository = A.Fake<IPatientTreatmentRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _noteRepositoryMock = new Mock<INoteRepository>();
            _patientFileRepositoryMock = new Mock<IPatientFileRepository>();

            //SUT
            _controller = new DoctorController(_httpContextAccessor, _doctorDashboardCardsRepository, _patientRepository, _roleRepository!, _suburbRepository, _patientFileRepository, _noteRepository, _medicationRepository, _scriptRepository, _scriptDetailRepository, _patientTreatmentRepository);

            _mockController = new DoctorController(_httpContextAccessor, _doctorDashboardCardsRepository, _patientRepositoryMock.Object, _roleRepository!, _suburbRepository, _patientFileRepositoryMock.Object, _noteRepositoryMock.Object, _medicationRepository, _scriptRepository, _scriptDetailRepository, _patientTreatmentRepository);


        }

        [Fact]
        public void AddReferral_Post_Returns_ViewResult()
        {
            // Arrange
            var patient = new Patient
            {
                UserID = 1,
                FirstName = "Brilliant",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Email = "brilliant@example.com",
                ResidentialAddress ="53 Gomery Avenue",
                SuburbID = 1,
                Status = "Referred"
            };

            // Act
            var result = _mockController.AddReferral(patient);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.AddReferralAsync(patient), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patient.Should().NotBeNull();
        }

        [Fact]
        public void ListReferral_Returns_ViewResult()
        {
            //Arrange
            var referrals = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _patientRepository.GetAllReferralsAsync()).Returns(referrals);

            //Act
            var result = _controller.ListReferral();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ViewReferral_Returns_ViewResultID()
        {
            //Arrange
            var referralById = A.Fake<VMPatient>();
            A.CallTo(() => _patientRepository.GetReferralByIdAsync(1)).Returns(referralById);

            //Act
            var result = _controller.ViewReferral(1);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void UpdateReferral_Post_Returns_ViewResult()
        {
            // Arrange
            var patient = new VMPatient
            {
                UserID = 1,
                FirstName = "Nhlamolo",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Email = "brilliant@example.com",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Referred"
            };

            // Act
            var result = _mockController.UpdateReferral(patient);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.UpdateReferralAsync(patient), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patient.Should().NotBeNull();
        }


        [Fact]
        public void ListRemovedReferral_Returns_ViewResult()
        {
             //Arrange
            var referrals = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _patientRepository.GetAllRemovedReferralsAsync()).Returns(referrals);

            //Act
            var result = _controller.ListRemovedReferral();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ViewRemovedReferral_Returns_ViewResultID()
        {
            //Arrange
            var visit = A.Fake<VMPatient>();
            A.CallTo(() => _patientRepository.GetRemovedReferralByIdAsync(1)).Returns(visit);

            //Act
            var result = _controller.ViewRemovedReferral(1);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void DeleteReferral_Post_Returns_ViewResult()
        {
            // Arrange
            var patient = new VMPatient
            {
                UserID = 1,
                FirstName = "Nhlamolo",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Email = "brilliant@example.com",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Removed"
            };

            // Act
            var result = _mockController.DeleteReferral(patient);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.DeleteReferralAsync(patient), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patient.Should().NotBeNull();
        }

        [Fact]
        public void ListDischargedPatients_Returns_ViewResult()
        {
            //Arrange
            var dischargedPatients = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _patientRepository.GetAllDischargedPatientsAsync()).Returns(dischargedPatients);

            //Act
            var result = _controller.ListDischargedPatients();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ViewDischargedPatient_Returns_ViewResultID()
        {
            //Arrange
            var dischargePatient = A.Fake<VMPatient>();
            A.CallTo(() => _patientRepository.GetDischargedPatientByIdAsync(1)).Returns(dischargePatient);

            //Act
            var result = _controller.ViewDischargedPatient(1);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void DischargePatient_Post_Returns_ViewResult()
        {
            // Arrange
            var patientFile = new VMPatientFile
            {

                FileID = 1,
                BedID = 4,
                ArrivalDate = DateTime.Now,
                Status = "Ready",
                PatientID = 2023

            };

            // Act
            var result = _mockController.DischargePatient(patientFile);

            // Assert
            _patientFileRepositoryMock.Verify(repo => repo.UpdateDischargePatientFileStatusAsync(patientFile), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patientFile.Should().NotBeNull();
        }

        [Fact]
        public void RetrieveReferral_Post_Returns_ViewResult()
        {
            // Arrange
            var patient = new VMPatient
            {
                UserID = 1,
                FirstName = "Nhlamolo",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Email = "brilliant@example.com",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Referred"
            };

            // Act
            var result = _mockController.RetrieveReferral(patient);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.RetrieveReferralAsync(patient), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patient.Should().NotBeNull();
        }

        [Fact]
        public void ReferPatientAgain_Post_Returns_ViewResult()
        {
            // Arrange
            var patient = new VMPatient
            {
                UserID = 1,
                FirstName = "Nhlamolo",
                LastName = "Motjiang",
                IDNumber = "9912024514251",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                Email = "brilliant@example.com",
                ResidentialAddress = "53 Gomery Avenue",
                SuburbID = 1,
                Status = "Referred"
            };

            // Act
            var result = _mockController.ReferPatientAgain(patient);

            // Assert
            _patientRepositoryMock.Verify(repo => repo.ReferPatientAsync(patient), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patient.Should().NotBeNull();
        }

    }
}
