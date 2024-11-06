using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Repository.Ward_Aministrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Repository.Doctor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.Common;
using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.WardAdministror;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using DataAccesslayer.Services;


namespace Atlegang_Medicare.Controllers.Ward_Administrator
{
    [Authorize(1,3)]
    public class WardAdministratorController : Controller
    {
        private readonly IBedRepository _bedRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientAllergyRepository _patientAllergyRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IPatientMovementRepository _patientMovementRepository;
        private readonly IPatientConditionRepository _patientConditionRepository;
        private readonly ISuburbRepository _suburbRepository;
        private readonly IAllergyRepository _allergyRepository;
        private readonly IChronicChonditionRepository _chronicChonditionRepository;
        private readonly IPatientMedicationRepository _patientMedicationRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IWardAdministratorDashboardCardsRepository _wardAdministratorDashboardCardsRepository;

        public WardAdministratorController(IWardAdministratorDashboardCardsRepository wardAdministratorDashboardCardsRepository, IMedicationRepository medicationRepository, IPatientConditionRepository patientConditionRepository, IPatientAllergyRepository patientAllergyRepository, IAllergyRepository allergyRepository, IChronicChonditionRepository chronicChonditionRepository, IPatientMovementRepository patientMovementRepository, ISuburbRepository suburbRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IUserRepository userRepository, IPatientFileRepository patientFileRepository, IBedRepository bedRepository, IPatientMedicationRepository patientMedicationRepository)
        {
            _wardAdministratorDashboardCardsRepository = wardAdministratorDashboardCardsRepository;
            _medicationRepository = medicationRepository;
            _allergyRepository = allergyRepository;
            _bedRepository = bedRepository;
            _patientMedicationRepository = patientMedicationRepository;
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _patientFileRepository = patientFileRepository;
            _patientMovementRepository = patientMovementRepository;
            _patientAllergyRepository = patientAllergyRepository;
            _chronicChonditionRepository = chronicChonditionRepository;
            _patientConditionRepository = patientConditionRepository;
            _suburbRepository = suburbRepository;
        }



        //Dashboard
        public async Task<IActionResult> Index()
        {
            ViewBag.DoctorList = await _wardAdministratorDashboardCardsRepository.GetAllDoctorsAsync();
            var wardAdministratorDashboardCadrs = await _wardAdministratorDashboardCardsRepository.GetAdminiStratorDashboardCardsAsync();
            return View(wardAdministratorDashboardCadrs);
        }
        //End of Dashboard




        //View Referred Patients
        public async Task<IActionResult> Referrals()
        {
            var referral = await _patientRepository.GetAllReferralsAsync();

            return View(referral);
        }
        //End of Referred Patients




        //Add Patient File
        public async Task<IActionResult> AddPatientFile(int userID)
        {
            //Get available beds
            ViewBag.Beds = await GetBedsDropDownList();
            //Get all active Doctors
            ViewBag.Doctors = await GetAllDoctors();
            //Get the user ID 
            ViewBag.PatientID = userID;
            var patient = await _patientRepository.GetReferralByIdAsync(userID);
            //Display the name of the patient
            ViewBag.ReferredPatient = patient.PatientName;

            var chronicsList = await _chronicChonditionRepository.GetAllChronicAsync();
            var medicationList = await _medicationRepository.GetAllMedicationsAsync();
            var allergyList = await _allergyRepository.GetAllAllergyAsync();

            ViewBag.ChronicConditions = new SelectList(chronicsList, "ConditionID", "ChronicType");
            ViewBag.Medication = new SelectList(medicationList, "MedicationID", "Name");
            ViewBag.Allergies = new SelectList(allergyList, "AllergyID", "Description");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPatientFile(PatientFile patientFile, IEnumerable<int>? selectedAllergies, IEnumerable<int>? selectedMedication, IEnumerable<int>? selectedChronics)
        {
            try
            {
                if (selectedAllergies != null)
                {
                    foreach (var allergy in selectedAllergies)
                    {
                        var patientAllergy = new PatientAllergy
                        {
                            AllergyID = allergy,
                            PatientID = patientFile.PatientID
                        };

                        await _patientAllergyRepository.AddPatientAllergyAsync(patientAllergy);
                    }
                }

                if (selectedChronics != null)
                {
                    foreach (var chronic in selectedChronics)
                    {
                        var patientCondition = new PatientCondition
                        {
                            ConditionID = chronic,
                            PatientID = patientFile.PatientID
                        };

                        await _patientConditionRepository.AddPatientConditionAsync(patientCondition);
                    }
                }

                if (selectedMedication != null)
                {
                    foreach (var medication in selectedMedication)
                    {
                        var patientMedication = new PatientMedication
                        {
                            MedicationID = medication,
                            PatientID = patientFile.PatientID
                        };

                        await _patientMedicationRepository.AddPatientMedicationAsync(patientMedication);
                    }
                }

                bool result = await _patientFileRepository.AddPatientFilesAsync(patientFile);


                if (result)
                {
                    TempData["TempSuccess"] = "Patient Admitted ";
                }
                else
                {
                    TempData["TempError"] = "Error Admitting Patient";
                    //Get available beds
                    ViewBag.Beds = await GetBedsDropDownList();
                    //Get all active Doctors
                    ViewBag.Doctors = await GetAllDoctors();

                }

            }
            catch
            {

            }

            var chronicsList = await _chronicChonditionRepository.GetAllChronicAsync();
            var medicationList = await _medicationRepository.GetAllMedicationsAsync();
            var allergyList = await _allergyRepository.GetAllAllergyAsync();
            ViewBag.ChronicConditions = new SelectList(chronicsList, "ConditionID", "ChronicType");
            ViewBag.Medication = new SelectList(medicationList, "MedicationID", "Name");
            ViewBag.Allergies = new SelectList(allergyList, "AllergyID", "Description");

            return View();

        }


        public async Task<IActionResult> ListDischargePatient()//for discharge
        {
            var patientFile = await _patientFileRepository.GetDischargeListAsync();
            return View(patientFile);
        }


        public async Task<IActionResult> UpdatePatientFile(int id)
        {
            //Get available beds
            ViewBag.Beds = await GetBedsDropDownList();
            //Get all active Doctors
            ViewBag.Doctors = await GetAllDoctors();

            if (id != 0)
            {
                var patientFile = await _patientFileRepository.GetPatientFileById(id);
                if (patientFile != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatientFile
                    {
                        PatientID = patientFile.PatientID,
                        PatientName = patientFile.PatientName,
                        BedID = patientFile.BedID,
                        DoctorID = patientFile.DoctorID
                    };

                    return View(viewModel);
                }
            }

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdatePatientFile(VMPatientFile patientFile)
        {
            try
            {
                if (patientFile != null)
                {
                    var viewModel = new PatientFile
                    {
                        DoctorID = patientFile.DoctorID
                    };

                    bool result = await _patientFileRepository.UpdatePatientFileAsync(patientFile);

                    if (result)
                    {
                        TempData["TempSuccess"] = "Patient File Updated ";
                        //Get available beds
                        ViewBag.Beds = await GetBedsDropDownList();
                        //Get all active Doctors
                        ViewBag.Doctors = await GetAllDoctors();

                    }
                    else
                    {
                        TempData["TempError"] = "Error Updating Patient File ";
                        //Get available beds
                        ViewBag.Beds = await GetBedsDropDownList();
                        //Get all active Doctors
                        ViewBag.Doctors = await GetAllDoctors();

                    }
                }
            }
            catch
            {
                //code error
            }

            return View(patientFile);
        }

        //End of Add Patient File






        //List Patient Files
        public async Task<IActionResult> ListPatientFile()// for view patient list whether  its closed or not
        {
            var patientFile = await _patientFileRepository.GetAllPatientFilewithOutStatusAsync();
            return View(patientFile);
        }
        //End of Patient Files






        //Add Patient movement
        public async Task<IActionResult> AddMovement()
        {
            ViewBag.PatientName = await GetPatientFileDropDownList(); // Pass dropdown list to the view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovement(Movement movement)
        {
            try
            {
                bool result = await _patientMovementRepository.AddMovementAsync(movement);

                if (result)
                {
                    TempData["TempSuccess"] = "Patient movement added ";
                }
                else
                {
                    TempData["TempError"] = "Error adding Patient Movement ";
                    ViewBag.PatientName = await GetPatientFileDropDownList(); // Pass dropdown list to the view
                }
            }
            catch
            {
                //code error
            }

            return View(movement);

        }

        public async Task<IActionResult> ListMovements()
        {
            var patientList = await _patientMovementRepository.GetAllMovementAsync();
            return View(patientList);
        }
        public async Task<IActionResult> UpdateMovement(int id)
        {
            TempData["TempSuccess"] = null;
            ViewBag.StatusValue = GetStatusDropDownList();

            if (id != 0)
            {
                var patient = await _patientMovementRepository.GetMovementByIdAsync(id);

                if (patient != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatientMovement
                    {
                        MovementID = patient.MovementID,
                        PatientName = patient.PatientName,
                        FileID = patient.FileID,
                        Description = patient.Description,
                        Date = patient.Date,
                        Status = patient.Status
                    };

                    return View(viewModel);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMovement(VMPatientMovement viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var patient = new Movement
                    {
                        MovementID = viewModel.MovementID,
                        FileID = viewModel.FileID,
                        Description = viewModel.Description,
                        Date = viewModel.Date,
                        Status = viewModel.Status
                    };

                    bool result = await _patientMovementRepository.UpdateMovementAsync(viewModel);

                    if (result)
                    {
                        TempData["TempSuccess"] = "Patient Movement updated ";
                    }
                    else
                    {
                        TempData["TempError"] = "Failed to update Patient Movement, please try again";
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View(viewModel);
        }


        private async Task<IEnumerable<SelectListItem>> GetPatientFileDropDownList()
        {
            var patientFiles = await _patientFileRepository.GetAllPatientFilesForMovement(); // Get all patient files

            // Create a dropdown list for patient files
            return patientFiles.Select(patientFile => new SelectListItem
            {
                Value = patientFile.FileID.ToString(),
                Text = patientFile.PatientName
            });
        }
        private IEnumerable<SelectListItem> GetStatusDropDownList()
        {
            List<string> statuses = new List<string>
            {
                "Available",
                "Not Available",
                "Theatre",
                "X-rays "
            };

            return statuses.Select(status => new SelectListItem
            {
                Value = status,
                Text = status
            });
        }
        //End of Patient movement




        private async Task<IEnumerable<SelectListItem>> GetAllReferredPatients()
        {
            var referredPatientName = await _patientFileRepository.GetAllReferredPatientNames();

            return referredPatientName.Select(referredPatientName => new SelectListItem
            {
                Value = referredPatientName.UserID.ToString(),
                Text = referredPatientName.PatientName
            });
        }
        // Get Doctors Details
        private async Task<IEnumerable<SelectListItem>> GetSuburbsDropDownList()
        {
            var suburbs = await _suburbRepository.GetAllSuburbsAsync();

            return suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbID.ToString(),
                Text = suburb.SuburbName
            });
        }
        private async Task<IEnumerable<SelectListItem>> GetAllDoctors()
        {
            var doctor = await _doctorRepository.GetAllDoctorsByNameAsync();

            return doctor.Select(doctor => new SelectListItem
            {
                Value = doctor.UserID.ToString(),
                Text = doctor.FullName
            });

        }

        private async Task<IEnumerable<SelectListItem>> GetAdmittedPatientMovement()
        {
            var admittedPatient = await _patientFileRepository.GetAlladmittedPatientMovement();
            return admittedPatient.Select(admittedPatient => new SelectListItem
            {
                Value = admittedPatient.FileID.ToString(),
                //Text = admittedPatient.FirstName.ToString(),

            });
        }
        private async Task<IEnumerable<SelectListItem>> GetBedsDropDownList()
        {
            var bed = await _bedRepository.GetAllBedsAsync();

            return bed.Select(bed => new SelectListItem
            {
                Value = bed.BedID.ToString(),
                Text = bed.BedID.ToString()
            });
        }
        private async Task<IEnumerable<SelectListItem>> GetAllergiesDropDownList()
        {

            var allergies = await _allergyRepository.GetAllAllergyAsync();

            return allergies.Select(allergies => new SelectListItem
            {
                Value = allergies.AllergyID.ToString(),
                Text = allergies.Description.ToString()
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetChronicConditionsDropDownList()
        {
            var chronicConditions = await _chronicChonditionRepository.GetAllChronicAsync();

            return chronicConditions.Select(chronicConditions => new SelectListItem
            {
                Value = chronicConditions.ConditionID.ToString(),
                Text = chronicConditions.ChronicType.ToString()
            });



        }
        private async Task<IEnumerable<SelectListItem>> MedicationDropDownList()
        {

            var medication = await _medicationRepository.GetAllMedicationsAsync();

            return medication.Select(medication => new SelectListItem
            {
                Value = medication.MedicationID.ToString(),
                Text = medication.Name.ToString()
            });

        }

        public async Task<IActionResult> PatientFileDischarge(int id)
        {
            if (id != 0)
            {
                var patientFiles = await _patientFileRepository.GetPatientFileByIdReady(id);

                if (patientFiles != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatientFile
                    {
                        LastName = patientFiles.LastName,
                        FirstName = patientFiles.FirstName,
                        Status = patientFiles.Status,
                        BedID = patientFiles.BedID,
                        WardName = patientFiles.WardName,
                        PatientID = patientFiles.PatientID,
                        FileID = patientFiles.FileID , // Include FileID
                        ArrivalDate = patientFiles.ArrivalDate
                    };

                    return View(viewModel);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PatientFileDischarge(VMPatientFile viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var patient = new PatientFile
                    {
                        Status = viewModel.Status,
                        BedID = viewModel.BedID,
                        PatientID = viewModel.PatientID,
                        FileID = viewModel.FileID

                    };

                    bool result = await _patientFileRepository.UpdatePatientFileDichargeAsync(viewModel);

                    if (result)
                    {
                        TempData["TempSuccess"] = "Patient successfully discharged.";
                    }
                    else
                    {
                        TempData["TempError"] = "Failed to discharge patient, please try again.";
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View(viewModel);
        }
      
}
}



