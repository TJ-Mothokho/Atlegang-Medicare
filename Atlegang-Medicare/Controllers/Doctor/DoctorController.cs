using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.View_Models.ConsumerManager;
using DataAccesslayer.Services;

namespace Atlegang_Medicare.Controllers.Doctor
{
    public class DoctorController : Controller
    {
        private readonly IDoctorDashboardCardsRepository _doctorDashboardCardsRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISuburbRepository _suburbRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IHttpContextAccessor _httpClient;
        private readonly INoteRepository _noteRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IScriptDetailRepository _scriptDetailRepository;
        private readonly IPatientTreatmentRepository _patientTreatmentRepository;

        public DoctorController(IHttpContextAccessor httpClient, IDoctorDashboardCardsRepository doctorDashboardCardsRepository, IPatientRepository patientRepository,
            IRoleRepository roleRepository, ISuburbRepository suburbRepository, IPatientFileRepository patientFileRepository, INoteRepository noteRepository,
            IMedicationRepository medicationRepository, IScriptRepository scriptRepository, IScriptDetailRepository scriptDetailRepository, 
            IPatientTreatmentRepository patientTreatmentRepository)
        {
            _doctorDashboardCardsRepository = doctorDashboardCardsRepository;
            _patientRepository = patientRepository;
            _roleRepository = roleRepository;
            _suburbRepository = suburbRepository;
            _patientFileRepository = patientFileRepository;
            _httpClient = httpClient;
            _noteRepository = noteRepository;
            _medicationRepository = medicationRepository;
            _scriptDetailRepository = scriptDetailRepository;
            _scriptRepository = scriptRepository;
            _patientTreatmentRepository = patientTreatmentRepository;
        }


        [Authorize(1,2)]

        //Dashboard
        public async Task<IActionResult> Index()
        {
            var doctorDashboardCards = await _doctorDashboardCardsRepository.GetDoctorDashboardCardsAsync();
            return View(doctorDashboardCards);
        }
        //End of Dashboard

        [Authorize(1, 2)]
        //Add Referral
        public async Task<IActionResult> AddReferral()
        {
            TempData["SuccessAlert"] = null;

            ViewBag.RoleList = GetRoleDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReferral(Patient patient)
        {
            try
            {
                patient.RoleID = 8;
                bool result = await _patientRepository.AddReferralAsync(patient);

                if (result)
                {
                    TempData["SuccessAlert"] = "Patient Referred ";
                }
                else
                {
                    TempData["ErrorAlert"] = "Failed to refer Patient, please try again";
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View();

        }
        [Authorize(1, 2)]
        public async Task<IActionResult> ListReferral()
        {
            var referral = await _patientRepository.GetAllReferralsAsync();
            return View(referral);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> ViewReferral(int id)
        {
            var referral = await _patientRepository.GetReferralByIdAsync(id);
            return View(referral);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> UpdateReferral(int id)
        {
            TempData["SuccessAlertUpdate"] = null;

            ViewBag.RoleList = GetRoleDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();

            if (id != 0)
            {
                var referral = await _patientRepository.GetReferralByIdAsync(id);
                if (referral != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatient
                    {
                        UserID = referral.UserID,
                        LastName = referral.LastName,
                        FirstName = referral.FirstName,
                        IDNumber = referral.IDNumber,
                        Gender = referral.Gender,
                        Email = referral.Email,
                        PhoneNumbers = referral.PhoneNumbers,
                        DateOfBirth = referral.DateOfBirth,
                        ResidentialAddress = referral.ResidentialAddress,
                        SuburbID = referral.SuburbID,
                        PatientID = referral.PatientID
                    };

                    return View(viewModel);
                }
            }

            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> UpdateReferral(VMPatient viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var patient = new Patient
                    {
                        UserID = viewModel.UserID,
                        LastName = viewModel.LastName,
                        FirstName = viewModel.FirstName,
                        IDNumber = viewModel.IDNumber,
                        Gender = viewModel.Gender,
                        Email = viewModel.Email,
                        PhoneNumbers = viewModel.PhoneNumbers,
                        DateOfBirth = viewModel.DateOfBirth,
                        ResidentialAddress = viewModel.ResidentialAddress,
                        SuburbID = viewModel.SuburbID,
                        PatientID = viewModel.PatientID

                    };

                    bool result = await _patientRepository.UpdateReferralAsync(viewModel);

                    if (result)
                    {
                        TempData["SuccessAlertUpdate"] = "Patient updated ";
                        ViewBag.SuburbList = await GetSuburbsDropDownList();
                        ViewBag.GenderList = GetGenderDropDownList();
                    }
                    else
                    {
                        TempData["ErrorAlertUpdate"] = "Failed to update Patient, please try again";
                        ViewBag.SuburbList = await GetSuburbsDropDownList();
                        ViewBag.GenderList = GetGenderDropDownList();
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View(viewModel);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> DeleteReferral(int id)
        {
            TempData["SuccessAlert"] = null;


            ViewBag.RoleList = GetRoleDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();

            if (id != 0)
            {
                var referral = await _patientRepository.GetReferralByIdAsync(id);
                if (referral != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatient
                    {
                        UserID = referral.UserID,
                        LastName = referral.LastName,
                        FirstName = referral.FirstName,
                        IDNumber = referral.IDNumber,
                        Gender = referral.Gender,
                        Email = referral.Email,
                        PhoneNumbers = referral.PhoneNumbers,
                        DateOfBirth = referral.DateOfBirth,
                        ResidentialAddress = referral.ResidentialAddress,
                        SuburbID = referral.SuburbID,
                        PatientID = referral.PatientID
                    };

                    return View(viewModel);
                }
            }

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> DeleteReferral(VMPatient patient)
        {
            try
            {
                bool result = await _patientRepository.DeleteReferralAsync(patient);

                if (result)
                {
                    TempData["SuccessAlert"] = "Patient Referral Cancelled ";
                }
                else
                {
                    TempData["ErrorAlert"] = "Failed to cancel patient referral, please try again";

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return RedirectToAction("ListReferral");
        }
        //End of Add Referral

        [Authorize(1, 2)]
        //List Cancelled Referrals
        public async Task<IActionResult> ListRemovedReferral()
        {
            var removedReferral = await _patientRepository.GetAllRemovedReferralsAsync();
            return View(removedReferral);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> ViewRemovedReferral(int id)
        {
            var referral = await _patientRepository.GetRemovedReferralByIdAsync(id);
            return View(referral);
        }
        //End of Cancelled Referrals


        [Authorize(1, 2)]
        //List Discharged Patients
        public async Task<IActionResult> ListDischargedPatients()
        {
            var discharged = await _patientRepository.GetAllDischargedPatientsAsync();
            return View(discharged);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> ViewDischargedPatient(int id)
        {
            var patient = await _patientRepository.GetDischargedPatientByIdAsync(id);
            return View(patient);
        }
        //End of Discharged Patients


        [Authorize(1, 2)]
        //Refer Patient Again
        public async Task<IActionResult> ReferPatientAgain(int id)
        {
            TempData["SuccessAlert"] = null;

            ViewBag.RoleList = GetRoleDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();

            if (id != 0)
            {
                var referral = await _patientRepository.GetDischargedPatientByIdAsync(id);
                if (referral != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatient
                    {
                        UserID = referral.UserID,
                        LastName = referral.LastName,
                        FirstName = referral.FirstName,
                        IDNumber = referral.IDNumber,
                        Gender = referral.Gender,
                        Email = referral.Email,
                        PhoneNumbers = referral.PhoneNumbers,
                        DateOfBirth = referral.DateOfBirth,
                        ResidentialAddress = referral.ResidentialAddress,
                        SuburbID = referral.SuburbID,
                        PatientID = referral.PatientID
                    };

                    return View(viewModel);
                }
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> ReferPatientAgain(VMPatient patient)
        {
            try
            {

                bool result = await _patientRepository.ReferPatientAsync(patient);

                if (result)
                {

                    return RedirectToAction("ListDischargedPatients");
                }
                else
                {
                    //code error
                }

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("ListDischargedPatients");

        }
        //End of Refer Patient Again






        [Authorize(1, 2)]
        //Retrieve Referral
        public async Task<IActionResult> RetrieveReferral(int id)
        {
            TempData["SuccessAlert"] = null;

            ViewBag.RoleList = GetRoleDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();

            if (id != 0)
            {
                var referral = await _patientRepository.GetRemovedReferralByIdAsync(id);
                if (referral != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatient
                    {
                        UserID = referral.UserID,
                        LastName = referral.LastName,
                        FirstName = referral.FirstName,
                        IDNumber = referral.IDNumber,
                        Gender = referral.Gender,
                        Email = referral.Email,
                        PhoneNumbers = referral.PhoneNumbers,
                        DateOfBirth = referral.DateOfBirth,
                        ResidentialAddress = referral.ResidentialAddress,
                        SuburbID = referral.SuburbID,
                        PatientID = referral.PatientID
                    };

                    return View(viewModel);
                }
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> RetrieveReferral(VMPatient patient)
        {
            bool result = await _patientRepository.RetrieveReferralAsync(patient);

            if (result)
            {
                TempData["SuccessAlert"] = "Patient Retrieved ";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return RedirectToAction("ListRemovedReferral");
        }
        //End of Retrieve Referral





        [Authorize(1, 2)]
        //List Patient Files
        public async Task<IActionResult> ListPatientFiles()
        {
            var userID = HttpContext.Session.GetInt32("UserID").Value;
            var patientFileList = await _patientFileRepository.GetAllPatientFilesForDoctor(userID);
            return View(patientFileList);
        }
        //End of List Patient Files




        [Authorize(1, 2,3,6,7)]
        //Patient Details
        public async Task<IActionResult> PatientDetails(int id)
        {
            ViewBag.PatientNotes = await _noteRepository.GetPatientNoteByFileID(id);
            ViewBag.PatientTreatments = await _patientTreatmentRepository.GetPatientTreatmentByIdAsync(id);
            ViewBag.PatientVitals = await _patientTreatmentRepository.GetPatientVitalsByIdAsync(id);
            ViewBag.PatientMovement = await _patientRepository.GetPatientMovementByIdAsync(id);
            var patientFile = await _patientFileRepository.GetPatientFileById(id);
            ViewBag.PatientConditions = await _patientRepository.GetPatientConditionsByIdAsync(patientFile.PatientID);
            ViewBag.PatientAllergies = await _patientRepository.GetPatientAllergiesByIdAsync(patientFile.PatientID);
            ViewBag.PatientMedication = await _patientRepository.GetPatientMedicationsByIdAsync(patientFile.PatientID);
            return View(patientFile);
        }
        //End of Patient Details



        [Authorize(1, 2)]
        //Discharge Patient
        public async Task<IActionResult> DischargePatient(int id)
        {

            TempData["SuccessAlert"] = null;

            if (id != 0)
            {
                var referral = await _patientFileRepository.GetDischargeById(id);

                if (referral != null)
                {
                    // Map Patient to PatientViewModel
                    var viewModel = new VMPatientFile
                    {
                        LastName = referral.LastName,
                        FirstName = referral.FirstName,
                        FileID = referral.FileID,
                        Gender = referral.Gender,
                        IDNumber = referral.IDNumber,
                        DateOfBirth = referral.DateOfBirth,
                        Status = referral.Status,
                        ArrivalDate = referral.ArrivalDate,
                        BedID = referral.BedID,
                        WardName = referral.WardName,
                        PatientID = referral.PatientID,
                    };

                    return View(viewModel);
                }
            }

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> DischargePatient(VMPatientFile viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    var patient = new PatientFile
                    {
                        
                        FileID = viewModel.FileID,
                        BedID = viewModel.BedID,
                        ArrivalDate = viewModel.ArrivalDate,
                        Status = viewModel.Status,
                        PatientID = viewModel.PatientID

                    };

                    bool result = await _patientFileRepository.UpdateDischargePatientFileStatusAsync(viewModel);

                    if (result)
                    {
                        TempData["SuccessAlert"] = "Patient Ready to Discharge ";
                    }
                    else
                    {
                        TempData["ErrorAlert"] = "Failed to discharge patient, please try again";
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View(viewModel);
        }

        //End of Discharge Patient





        //Prescription
        public static List<ScriptDetail> listOfScripts = new List<ScriptDetail>();
        public static int FileID;
        private IDoctorDashboardCardsRepository doctorDashboardCardsRepository;
        [Authorize(1, 2)]
        public async Task<IActionResult> AddPrescription(int fileID)
        {
            TempData["SuccessAlert"] = null;

            var medication = await _medicationRepository.GetAllMedicationsAsync();
            ViewBag.MedicationDropDown = new SelectList(medication, "MedicationID", "Name");

            //Save the orderdetails in a Viewbag to show in the view. Using LINQ to join the Consumables and OrderDetails to display the Consumable Name
            ViewBag.PrescriptionItems = from item in listOfScripts
                                join meds in medication on item.MedicationID equals meds.MedicationID
                                select new
                                {
                                    MedicationName = meds.Name,
                                    item.Dosage,
                                    meds.Schedule
                                };
            if(fileID != 0)
            {
                ViewBag.FileID = fileID;
            }
            else if (FileID != 0)
            {
                ViewBag.FileID = FileID;
            }

            return View();
        }
        [Authorize(1, 2)]
        [HttpPost]
        public async Task<IActionResult> AddToPrescription(ScriptDetail script, int DosageNumber, string DosagePeriod, int fileID)
        {
            // Initialize listOfScripts if it's null
            if (listOfScripts == null)
            {
                listOfScripts = new List<ScriptDetail>();
            }
            script.Dosage = $"{DosageNumber} time(s) per {DosagePeriod}";

            listOfScripts.Add(script);

            TempData["SuccessAlert"] = null;

            var medication = await _medicationRepository.GetAllMedicationsAsync();
            ViewBag.MedicationDropDown = new SelectList(medication, "MedicationID", "Name");
            FileID = fileID;

            return RedirectToAction("AddPrescription");
        }
        [Authorize(1, 2)]
        [HttpPost]
        public async Task<IActionResult> Prescribe(int fileID)
        {
            //Get Medications from Database
            var medication = await _medicationRepository.GetAllMedicationsAsync();
            var script = new Script
            {
                ScriptDate = DateTime.Now,
                DoctorID = HttpContext.Session.GetInt32("UserID").Value,
                FileID = fileID
            };

            var result = await _scriptRepository.AddScriptAsync(script);

            if (result == true)
            {
                //Retrieve created Script by current doctor
                var recentScript = await _scriptRepository.GetScriptAdded(script.DoctorID);
                if (recentScript == null)
                {
                    TempData["Error"] = "Failed to retrieve Script.";
                    return View();
                }

                //Assign PatientVitalID
                foreach (var item in listOfScripts)
                {
                    //at this point, item contains the vitalID and vitalValue

                    var scriptDetails = new ScriptDetail
                    {
                        MedicationID = item.MedicationID,
                        ScriptID = recentScript.ScriptID,
                        Dosage = item.Dosage
                    };

                    //Create Script Details
                    var isAdded = await _scriptDetailRepository.AddScriptDetailAsync(scriptDetails);
                    TempData["Success"] = "Script Added Successfully";
                }

                TempData["Success"] = "Script Added Successfully";
            }
            else
            {
                TempData["Error"] = "script not added";
            }
            listOfScripts.Clear();
            return RedirectToAction("ListPatientFiles");
        }

        

        //Private methods
        //Get role
        private IEnumerable<SelectListItem> GetRoleDropDownList()
        {
            List<string> role = new List<string>
            {
                "Patient"
            };

            return role.Select(gender => new SelectListItem
            {
                Value = 8.ToString(),
                Text = gender
            });
        }
        //Get gender 
        private IEnumerable<SelectListItem> GetGenderDropDownList()
        {
            List<string> genders = new List<string>
            {
                "Male",
                "Female",
                "Other"
            };

            return genders.Select(gender => new SelectListItem
            {
                Value = gender,
                Text = gender
            });
        }
        //Get suburb
        private async Task<IEnumerable<SelectListItem>> GetSuburbsDropDownList()
        {
            var suburbs = await _suburbRepository.GetAllSuburbsAsync();

            return suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbID.ToString(),
                Text = suburb.SuburbName
            });
        }
    }
}
