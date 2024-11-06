using Atlegang_Medicare.Controllers.Doctor;
using Atlegang_Medicare.Controllers.Nurse;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.Models.View_Models.Doctor;

using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;


namespace Atlegang_Unit_Tests.Controllers.Nurse
{
    public class NurseControllerTest
    {
        private  INurseDashboardCardsRepository _nurseDashboardCardsRepository;
        private IPatientFileRepository _patientFileRepository;
        private  IVitalRepository _vitalRepository;
        private  IPatientVitalRepository _patientVitalRepository;
        private INoteRepository _noteRepository;
        private  IMedicationRepository _medicationRepository;
        private  ITreatmentRepository _treatmentRepository;
        private  IPatientTreatmentRepository _patientTreatmentRepository;
        private  IAdministerMedsRepository _administerMedsRepository;
        private  IHttpContextAccessor _httpClient;
        private  INurseRepository _nurseRepository;
        private IPatientVitalDetailsRepository _patientVitalDetailsRepository;
        private  IScriptRepository _scriptRepository;
        private  IScriptDetailRepository _scriptDetailRepository;


        private NurseController _controller;
        private NurseController _mockController;

        public NurseControllerTest()
        { 
            _nurseDashboardCardsRepository = A.Fake<INurseDashboardCardsRepository>();
            _noteRepository = A.Fake<INoteRepository>();
            _patientFileRepository = A.Fake<IPatientFileRepository>();
            _vitalRepository = A.Fake<IVitalRepository>();
            _patientVitalRepository = A.Fake<IPatientVitalRepository>();
            _medicationRepository = A.Fake<IMedicationRepository>();
            _treatmentRepository = A.Fake<ITreatmentRepository>();
            _patientTreatmentRepository = A.Fake<IPatientTreatmentRepository>();
            _administerMedsRepository = A.Fake<IAdministerMedsRepository>();
            _httpClient = A.Fake<HttpContextAccessor>();
            _patientVitalDetailsRepository = A.Fake<IPatientVitalDetailsRepository>();
            _scriptRepository = A.Fake<IScriptRepository>();
            _scriptDetailRepository = A.Fake<IScriptDetailRepository>();
            _nurseRepository=A.Fake<INurseRepository>();




            //_controller = new NurseController(_nurseDashboardCardsRepository, _noteRepository,_patientFileRepository,_vitalRepository, _patientVitalRepository,_medicationRepository, _treatmentRepository, _patientTreatmentRepository, _administerMedsRepository, _httpClient, _patientVitalDetailsRepository, _scriptRepository, _scriptDetailRepository , _nurseRepository);
            //_mockController = new NurseController(_nurseDashboardCardsRepository, _noteRepository, _patientFileRepository, _vitalRepository, _patientVitalRepository, _medicationRepository, _treatmentRepository, _patientTreatmentRepository, _administerMedsRepository, _httpClient, _patientVitalDetailsRepository, _scriptRepository, _scriptDetailRepository, _nurseRepository);

            _controller = new NurseController(_nurseDashboardCardsRepository, _scriptDetailRepository, _scriptRepository, _nurseRepository, _noteRepository, _patientFileRepository, _vitalRepository, _patientVitalRepository, _medicationRepository, _treatmentRepository, _administerMedsRepository, _patientTreatmentRepository, _httpClient, _patientVitalDetailsRepository);
            _mockController = new NurseController(_nurseDashboardCardsRepository, _scriptDetailRepository, _scriptRepository, _nurseRepository, _noteRepository, _patientFileRepository, _vitalRepository, _patientVitalRepository, _medicationRepository, _treatmentRepository, _administerMedsRepository, _patientTreatmentRepository, _httpClient, _patientVitalDetailsRepository);

        }

        [Fact]
        public void MedsAdministeredList_Returns_ViewResult()
        {
            //Arrange
            var patientList= A.Fake<IEnumerable<VMAdministerMeds>>();
            A.CallTo(() => _administerMedsRepository.GetAllMedsAdministeredByNurse()).Returns(patientList);

            //Act
            var result = _controller.MedsAdministeredList();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();


        }

        [Fact]
        public void AllMedicationsAdministerd_Returns_ViewResult()
        {
            // Arrange
            var fileId = 1; // Example file ID
            var mockMedsList = A.Fake<IEnumerable<VMAdministerMeds>>(); // Mocking the meds list
            A.CallTo(() => _administerMedsRepository.GetMedsAdministeredByFileId(fileId)).Returns(mockMedsList); // Setup fake method

            // Act
            var result =  _controller.AllMedicationsAdministerd(fileId);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>(); // Assert the result is of type ViewResult

          


        }


        [Fact]
        public void MedsAdministeredDetails_Returns_ViewResult()
        {
            // Arrange
            var id = 1; // Example file ID
            var details = A.Fake<VMAdministerMeds>(); // Mocking the meds list
            A.CallTo(() => _administerMedsRepository.GetMedsAdministeredById(id)).Returns(details); // Setup fake method

            // Act
            var result = _controller.MedsAdministeredDetails(id);

            // Assert
            result.Should().BeOfType<Task<IActionResult>>(); // Assert the result is of type ViewResult




        }

    
    //    [Fact]
    //    public async Task PatientScriptDetails__Returns_ViewResult()
    //    {

    //        // Arrange
    //        int scriptId = 1;
    //        int fileId = 1;


    //        // Fake patient data
    //        var fakePatient = A.Fake<VMPatientFile>();
    //        fakePatient.PatientName = "John Doe";

    //        // Fake script details data
    //        var fakeScriptDetails = new List<ScriptDetail>
    //{
    //    new ScriptDetail { MedicationID = 101 },
    //    new ScriptDetail { MedicationID = 102 }
    //};

    //        // Set up mock repository method calls
    //        A.CallTo(() => _patientFileRepository.GetPatientFileById(fileId))
    //            .Returns(Task.FromResult(fakePatient)); // Mock GetPatientFileById

    //        A.CallTo(() => _scriptDetailRepository.GetScriptDetailsByScriptIdAsync(scriptId))
    //            .Returns(Task.FromResult(fakeScriptDetails)); // Mock GetScriptDetailsByScriptIdAsync

    //        A.CallTo(() => _administerMedsRepository.GetMedsAdministerCountAsync(fileId, 101))
    //            .Returns(Task.FromResult(5)); // Mock GetMedsAdministerCountAsync for MedicationID 101

    //        A.CallTo(() => _administerMedsRepository.GetMedsAdministerCountAsync(fileId, 102))
    //            .Returns(Task.FromResult(3)); // Mock GetMedsAdministerCountAsync for MedicationID 102

    //        // Act
    //        var result = await _controller.PatientScriptDetails(scriptId, fileId);

    //        // Assert
    //        result.Should().BeOfType< Task<IActionResult>>(); // Ensure the result is a ViewResult

 

           

    //    }



    }
}
