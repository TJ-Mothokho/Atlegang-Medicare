using Atlegang_Medicare.Controllers.Doctor;
using Atlegang_Medicare.Controllers.Ward_Administrator;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.WardAdministror;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using DataAccesslayer.Repository.Ward_Administrator;
using DataAccesslayer.Repository.Ward_Aministrator;
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

namespace Atlegang_Unit_Tests.Controllers.WardAdministrator
{
    public class WardAdministratorControllerTest
    {
        // bring  all the indepedencies first 
        private WardAdministratorController _wardAdministratorController;
        private IPatientRepository _patientRepository;
        private IBedRepository _bedRepository;
        private IDoctorRepository _doctorRepository;
        private IUserRepository _userRepository;
        private IPatientAllergyRepository _patientAllergyRepository;
        private IPatientFileRepository _patientFileRepository;
        private IPatientMovementRepository _patientMovementRepository;
        private IPatientConditionRepository _patientConditionRepository;
        private ISuburbRepository _suburbRepository;
        private IAllergyRepository _allergyRepository;
        private IChronicChonditionRepository _chronicChonditionRepository;
        private IPatientMedicationRepository _patientMedicationRepository;
        private IMedicationRepository _medicationRepository;
        private IWardAdministratorDashboardCardsRepository _wardAdministratorDashboardCardsRepository;
        private HttpContextAccessor _httpContextAccessor;


        private Mock<IPatientFileRepository> _patientFileRepositoryMock;
        private Mock<IPatientMovementRepository> _patientMovementRepositoryMock;

        private WardAdministratorController _controller;
        private WardAdministratorController _mockController;

        public WardAdministratorControllerTest()
        {
            _patientRepository = A.Fake<IPatientRepository>();
            _bedRepository = A.Fake<IBedRepository>();
            _doctorRepository = A.Fake<IDoctorRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _patientAllergyRepository = A.Fake<IPatientAllergyRepository>();
            _patientFileRepository = A.Fake<IPatientFileRepository>();
            _patientMovementRepository = A.Fake<IPatientMovementRepository>();
            _patientConditionRepository = A.Fake<IPatientConditionRepository>();
            _suburbRepository = A.Fake<ISuburbRepository>();
            _allergyRepository = A.Fake<IAllergyRepository>();
            _chronicChonditionRepository = A.Fake<IChronicChonditionRepository>();
            _patientMedicationRepository = A.Fake<IPatientMedicationRepository>();
            _medicationRepository = A.Fake<IMedicationRepository>();
            _wardAdministratorDashboardCardsRepository = A.Fake<IWardAdministratorDashboardCardsRepository>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            _patientMovementRepositoryMock = new Mock<IPatientMovementRepository>();
            _patientFileRepositoryMock = new Mock<IPatientFileRepository>();

            //Suit
            _controller = new WardAdministratorController(_wardAdministratorDashboardCardsRepository, _medicationRepository, _patientConditionRepository, _patientAllergyRepository, _allergyRepository, _chronicChonditionRepository, _patientMovementRepository, _suburbRepository, _patientRepository, _doctorRepository, _userRepository, _patientFileRepository, _bedRepository, _patientMedicationRepository);

            _mockController = new WardAdministratorController(_wardAdministratorDashboardCardsRepository, _medicationRepository, _patientConditionRepository, _patientAllergyRepository, _allergyRepository, _chronicChonditionRepository, _patientMovementRepositoryMock.Object, _suburbRepository, _patientRepository, _doctorRepository, _userRepository, _patientFileRepositoryMock.Object, _bedRepository, _patientMedicationRepository);


        }

        [Fact]
        public void Referrals_Returns_ViewResult()
        {
            //Arrange
            var referrals = A.Fake<IEnumerable<dynamic>>();
            A.CallTo(() => _patientRepository.GetAllReferralsAsync()).Returns(referrals);

            //Act
            var result = _controller.Referrals();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ListDischargePatient_Returns_ViewResult()
        {
            //Arrange
            var dischargedPatientList = A.Fake<IEnumerable<VMPatientFile>>();
            A.CallTo(() => _patientFileRepository.GetDischargeListAsync()).Returns(dischargedPatientList);

            //Act
            var result = _controller.ListDischargePatient();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ListPatientFile_Returns_ViewResult()
        {
            //Arrange
            var patientFiles = A.Fake<IEnumerable<VMPatientFile>>();
            A.CallTo(() => _patientFileRepository.GetAllPatientFilewithOutStatusAsync()).Returns(patientFiles);

            //Act
            var result = _controller.ListPatientFile();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ListMovements_Returns_ViewResult()
        {
            //Arrange
            var patientMovements = A.Fake<IEnumerable<VMPatientMovement>>();
            A.CallTo(() => _patientMovementRepository.GetAllMovementAsync()).Returns(patientMovements);

            //Act
            var result = _controller.ListMovements();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void UpdatePatientFile_Post_Returns_ViewResult()
        {
            // Arrange
            var patientFile = new VMPatientFile
            {
               DoctorID = 1
            };

            // Act
            var result = _mockController.UpdatePatientFile(patientFile);

            // Assert
            _patientFileRepositoryMock.Verify(repo => repo.UpdatePatientFileAsync(patientFile), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patientFile.Should().NotBeNull();
        }
        [Fact]
        public void UpdateMovement_Post_Returns_ViewResult()
        {
            // Arrange
            var patientMovement = new VMPatientMovement
            {
                MovementID = 1,
                FileID = 3,
                Description = "X-ray",
                Date = DateTime.Now,
                Status = "Not Available"
            };

            // Act
            var result = _mockController.UpdateMovement(patientMovement);

            // Assert
            _patientMovementRepositoryMock.Verify(repo => repo.UpdateMovementAsync(patientMovement), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patientMovement.Should().NotBeNull();
        }
        [Fact]
        public void PatientFileDischarge_Post_Returns_ViewResult()
        {
            // Arrange
            var patientFile = new VMPatientFile
            {
                Status = "Closed",
                BedID = 5,
                PatientID =13,
                FileID =15

            };

            // Act
            var result = _mockController.PatientFileDischarge(patientFile);

            // Assert
            _patientFileRepositoryMock.Verify(repo => repo.UpdatePatientFileDichargeAsync(patientFile), Moq.Times.Once);
            result.Should().BeOfType<Task<IActionResult>>();
            var viewResult = result.Should().BeOfType<Task<IActionResult>>();

            patientFile.Should().NotBeNull();
        }
    }
}
