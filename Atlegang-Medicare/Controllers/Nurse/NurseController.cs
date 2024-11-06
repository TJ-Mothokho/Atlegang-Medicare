using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Services;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccesslayer.Models.Dashboards.Nurse;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Atlegang_Medicare.Models;
using System.Diagnostics;

namespace Atlegang_Medicare.Controllers.Nurse
{
    
    public class NurseController : Controller
    {


        // Define private readonly fields for repositories used in the controller
        private readonly INurseDashboardCardsRepository _nurseDashboardCardsRepository;
        private readonly IPatientFileRepository _patientFileRepository;
        private readonly IVitalRepository _vitalRepository;
        private readonly IPatientVitalRepository _patientVitalRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IPatientTreatmentRepository _patientTreatmentRepository;
        private readonly IAdministerMedsRepository _administerMedsRepository;
        private readonly IHttpContextAccessor _httpClient;
        private readonly INurseRepository _nurseRepository;
        private readonly IPatientVitalDetailsRepository _patientVitalDetailsRepository;
        private readonly IScriptRepository _scriptRepository;
        private readonly IScriptDetailRepository _scriptDetailRepository;

        // Constructor to initialize the repositories (dependency injection)
        public NurseController(INurseDashboardCardsRepository nurseDashboardCardsRepository,IScriptDetailRepository scriptDetailRepository ,IScriptRepository scriptRepository, INurseRepository nurseRepository, INoteRepository noteRepository, IPatientFileRepository patientFileRepository, IVitalRepository vitalRepository, IPatientVitalRepository patientVitalRepository, IMedicationRepository medicationRepository, ITreatmentRepository treatmentRepository, IAdministerMedsRepository administerMedsRepository, IPatientTreatmentRepository patientTreatmentRepository, IHttpContextAccessor httpContextAccessor, IPatientVitalDetailsRepository patientVitalDetailsRepository)
        {
            _nurseDashboardCardsRepository = nurseDashboardCardsRepository;
            _noteRepository = noteRepository;
            _patientFileRepository = patientFileRepository;
            _vitalRepository = vitalRepository;
            _patientVitalRepository = patientVitalRepository;
            _medicationRepository = medicationRepository;
            _treatmentRepository = treatmentRepository;
            _patientTreatmentRepository = patientTreatmentRepository;
            _administerMedsRepository = administerMedsRepository;
            _httpClient = httpContextAccessor;
            _patientVitalDetailsRepository = patientVitalDetailsRepository;
            _nurseRepository = nurseRepository;
            _scriptRepository = scriptRepository;
            _scriptDetailRepository = scriptDetailRepository;

        }

        [Authorize(1,6,7)]
        // Default action method to return the Index view
        public async Task<IActionResult> Index()
        {
            var nurseDashboardCards = await _nurseDashboardCardsRepository.GetNurseDashboardAsync();
            return View(nurseDashboardCards);
        }
        [Authorize(1,2, 6, 7)]
        // Method to list all notes. Fetches notes asynchronously from the repository and returns them to the view.
        public async Task<IActionResult> ListNotes()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            return View(notes);
        }
        [Authorize(1, 2, 6, 7)]
        // Method to display the form for adding a new note. Populates a dropdown list of patients for the view.
        public async Task<IActionResult> AddNote()
        {
            ViewBag.PatientName = await GetPatientFileDropDownList(); // Pass dropdown list to the view
            return View();
        }

        // POST method to add a new note. This handles form submission from the AddNote view.
        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            try
            {
                bool result = await _noteRepository.AddNoteAsync(note); // Add note to the repository

                if (result)
                {
                    return RedirectToAction("ListPatientNote"); // Redirect to list notes if successful
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There was an error adding a note. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during note addition
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            // If adding the note fails, return the view with the current data
            return View(note);
        }

        // Private method to get a list of patient files and return them as dropdown list items.
        private async Task<IEnumerable<SelectListItem>> GetPatientFileDropDownList()
        {
            var patientFiles = await _patientFileRepository.GetPatientFiles(); // Get all patient files

            // Create a dropdown list for patient files
            return patientFiles.Select(patientFile => new SelectListItem
            {
                Value = patientFile.FileID.ToString(),
                Text = patientFile.PatientName
            });
        }


        private async Task<IEnumerable<SelectListItem>> GetPatientTreatmentDropDownList()
        {
            var treatment = await _treatmentRepository.GetAllAsync(); // Get all patient files

            // Create a dropdown list for patient files
            return treatment.Select(treatment => new SelectListItem
            {
                Value = treatment.TreatmentID.ToString(),
                Text = treatment.Description
            });
        }
        [Authorize(1, 2, 6, 7)]
        // Method to list all patient notes, fetching them asynchronously from the repository.
        public async Task<IActionResult> ListPatientNote()
        {
            var patientNotes = await _noteRepository.GetAllNotesAsync();
            return View(patientNotes);
        }
        [Authorize(1, 2, 6, 7)]
        // Method to display the details of a specific patient note by its ID.
        public async Task<IActionResult> PatientNoteDetails(int id)
        {
            var patientNoteDetail = await _noteRepository.GetNoteByIdAsync(id);
            return View(patientNoteDetail);
        }
        [Authorize(1, 2, 6, 7)]

        // Method to display the form for updating an existing note. It fetches the note by ID.
        public async Task<IActionResult> UpdateNote(int id)
        {

            if (id != 0)
            {
                var patientNote = await _noteRepository.GetNoteByIdAsync(id);
                if (patientNote != null)
                {
                    // Create a ViewModel from the fetched patient note
                    var vMNote = new VMNote
                    {
                        NoteID = patientNote.NoteID,
                        FileID = patientNote.FileID,
                        Description = patientNote.Description,
                        Date = patientNote.Date
                    };

                    return View(vMNote); // Return the view populated with the note data
                }
            }
            return View(); // If no valid ID or note, return an empty form
        }

        // POST method to handle the form submission for updating a note.
        [HttpPost]
        public async Task<IActionResult> UpdateNote(VMNote viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    // Convert ViewModel back to the Note domain model
                    var note = new Note
                    {
                        NoteID = viewModel.NoteID,
                        FileID = viewModel.FileID,
                        Description = viewModel.Description,
                        Date = viewModel.Date
                    };

                    // Call the repository to update the note
                    bool result = await _noteRepository.UpdateNoteAsync(viewModel);

                    if (result)
                    {
                        return RedirectToAction("ListPatientNote"); // Redirect to the list of notes after successful update
                    }

                }
            }
            catch
            {
                // Handle any errors that occur during the update process
                ModelState.AddModelError("", "An error occurred while updating the note.");
            }

            return View(viewModel);
        }
        [Authorize(1, 2, 6, 7)]

        // Method to display the confirmation view for deleting a patient note.
        public async Task<IActionResult> DeletePatientNote(int id)
        {
            if (id != 0)
            {
                var patientNote = await _noteRepository.GetNoteByIdAsync(id);
                return View(patientNote); // Return the note details to confirm deletion
            }
            return View(); // If no valid ID, return an empty view
        }

        // POST method to handle the deletion of a patient note.
        [HttpPost]
        public async Task<IActionResult> DeletePatientNote(VMNote note)
        {
            bool result = await _noteRepository.DeleteNoteAsync(note.NoteID); // Call repository to delete the note

            if (result)
            {
                return RedirectToAction("ListPatientNote"); // Redirect to the list of notes after successful deletion
            }

            return RedirectToAction("ListPatientNote"); // If deletion fails, still redirect to the list
        }
        [Authorize(1, 6, 7)]

        // Method to list all patients, fetching patient files asynchronously from the repository.
        public async Task<IActionResult> PatientList()
        {
            var patients = await _patientFileRepository.GetPatientFiles();

            return View(patients); // Return the list of patients to the view
        }
        [Authorize(1, 6, 7)]

        public async Task<IActionResult> PatientsAdmittedToday()
        {
            var recentPatients = await _patientFileRepository.GetPatientsAdmittedToday();
            return View(recentPatients);
        }







        [Authorize(1, 6, 7)]
        public async Task<IActionResult> TreatmentList()
        {
            var treatments = await _patientTreatmentRepository.GetAllPatientTreatments();
            return View(treatments);


        }



        [Authorize(1, 6, 7)]
        //Patient Treatment methods
        public async Task<IActionResult> AddPatientTreatment(int id)
        {

            ViewBag.FileID = id;


            var patient = await _patientFileRepository.GetPatientFileById(id);
            ViewBag.PatientName = patient.PatientName;

            ViewBag.PatientTreatment = await GetPatientTreatmentDropDownList();


            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AddPatientTreatment(PatientTreatment patientTreatment, int id)
        {
            try
            {
                bool result = await _patientTreatmentRepository.AddPatientTreatment(patientTreatment);

                if (result)
                {
                    TempData["Success"] = "Added";

                    return RedirectToAction("AddPatientTreatment");

                }
            }
            catch
            {
                // Handle any errors that occur during the update process
                ModelState.AddModelError("", "An error occurred while adding the treatment.");
            }

            var patient = await _patientFileRepository.GetPatientFileById(id);

            ViewBag.PatientName = patient.PatientName;

            ViewBag.FileID = id;

             

            ViewBag.PatientTreatment = await GetPatientTreatmentDropDownList();
            return View();
        }


        [Authorize(1, 6, 7)]
        public async Task<IActionResult> PatientTreatmentDetails(int id)
        {
            var patientTreatment = await _patientTreatmentRepository.GetPatientTreatmentById(id);
            return View(patientTreatment);

        }
        [Authorize(1, 6, 7)]
        public async Task<IActionResult> PatientTreatmentFileDetails(int id)
        {
            var details = await _patientTreatmentRepository.GetPatientTreatmentByFileId(id);
            return View(details);

        }
        [Authorize(1, 6, 7)]

        public async Task<IActionResult> PatientsWithScripsList()
        {
            var patientScriptsList = await _scriptRepository.GetAllScriptsAsync();
            ViewBag.ScriptList = patientScriptsList;
            return View();


        }

        [Authorize(1, 6, 7)]
        public async Task<IActionResult> MedsAdministeredList()
        {
            var patientList = await _administerMedsRepository.GetAllMedsAdministeredByNurse();
            return View(patientList);


        }
        [Authorize(1, 6, 7)]
        public static int FileId;
        public static int ScriptId;
        public async Task<IActionResult> PatientScriptDetails(int scriptId, int fileId)
        {
            FileId = fileId;
            ScriptId = scriptId;

            var patient = await _patientFileRepository.GetPatientFileById(fileId);

            ViewBag.PatientName = patient.PatientName;

            var patientScripts = await _scriptDetailRepository.GetScriptDetailsByScriptIdAsync(scriptId);

            foreach (var item in patientScripts)
            {
                item.AdministerCount = await _administerMedsRepository.GetMedsAdministerCountAsync(FileId, item.MedicationID);
            }
            return View(patientScripts);

        }
        [Authorize(1, 6, 7)]
        public async Task<IActionResult> AddAdministerMeds(int nurseID, int medicationID)
        {
            try
            {
                var AdministerMeds = new MedsAdminister
                {
                    NurseID = nurseID,
                    MedicationID = medicationID,
                    FileID = FileId
                };

                var result = await _administerMedsRepository.AddAdministerMeds(AdministerMeds);

                if (result == true)
                {
                    TempData["Success"] = "Medication Administered";
                }
                else
                {
                    TempData["Error"] = "Medication not Administered";
                }

                return RedirectToAction("PatientScriptDetails", new {ScriptId, FileId});
            }
            catch (Exception ex)
            {
                return View();
            }

        }



        [Authorize(1, 6, 7)]
        public async Task<IActionResult> MedsAdministeredDetails(int id)
        {
            var details = await _administerMedsRepository.GetMedsAdministeredById(id);
            return View(details);

        }

        [Authorize(1, 6, 7)]

        public async Task<IActionResult> AllMedicationsAdministerd(int id)
        {

            var meds = await _administerMedsRepository.GetMedsAdministeredByFileId(id);
            return View(meds);

        }






 









        //VITALS METHODS

        // Method to generate a range of values for blood pressure
        public List<string> GenerateBloodPressureRange(int start, int end, int step)
        {
            var range = new List<string>();
            for (int systolic = start; systolic <= end; systolic += step)
            {
                for (int diastolic = start; diastolic <= end; diastolic += step)
                {
                    // Ensure diastolic is less than or equal to systolic
                    if (diastolic <= systolic)
                    {
                        range.Add($"{systolic}/{diastolic}");
                    }
                }
            }
            return range;
        }

        // Method to generate a range of temperature values
        public List<string> GenerateTemperatureRange(double start, double end, double step)
        {
            var range = new List<string>();
            for (double temp = start; temp <= end; temp += step)
            {
                range.Add($"{temp}");
            }
            return range;
        }

        // Method to generate a range of pulse rate values
        public List<string> GeneratePulseRateRange(int start, int end, int step)
        {
            var range = new List<string>();
            for (int rate = start; rate <= end; rate += step)
            {
                range.Add($"{rate}");
            }
            return range;
        }

        // Method to generate a range of blood sugar levels
        public List<string> GenerateBloodSugarRange(int start, int end, int step)
        {
            var range = new List<string>();
            for (int level = start; level <= end; level += step)
            {
                range.Add($"{level}");
            }
            return range;
        }


        // Method to generate a range of respiratory rate values
        public List<string> GenerateRespiratoryRateRange(int start, int end, int step)
        {
            var range = new List<string>();
            for (int rate = start; rate <= end; rate += step)
            {
                range.Add($"{rate}");
            }
            return range;
        }
        [Authorize(1, 6, 7)]
        public async Task<IActionResult> AddPatientVitals(int id)
        {
            // Prepare the data for each vital sign
            var heartRateOptions = GeneratePulseRateRange(40, 200, 10);
            var bloodPressureOptions = GenerateBloodPressureRange(90, 200, 10);
            var temperatureOptions = GenerateTemperatureRange(34.0, 41.0, 0.5);
            var respiratoryRateOptions = GenerateRespiratoryRateRange(10, 40, 2);
            var bloodSugarOptions = GenerateBloodSugarRange(60, 300, 10);

            // Pass the data to the view using ViewBag or ViewModel
            ViewBag.HeartRateOptions = new SelectList(heartRateOptions);
            ViewBag.BloodPressureOptions = new SelectList(bloodPressureOptions);
            ViewBag.TemperatureOptions = new SelectList(temperatureOptions);
            ViewBag.RespiratoryRateOptions = new SelectList(respiratoryRateOptions);
            ViewBag.BloodSugarOptions = new SelectList(bloodSugarOptions);

            ViewBag.FileID = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientVitals(int nurseID, int fileID, IEnumerable<PatientVitalDetails> vitals)
        {
            try
            {
                //Create Patient Vital
                var newPatientVital = new PatientVital
                {
                    FileID = fileID,
                    NurseID = nurseID,
                    Date = DateTime.Now
                };

                var result = await _patientVitalRepository.AddPatientVital(newPatientVital);

                if (result == true)
                {
                    //Retrieve created Patient Vital by current nurse
                    var patientVital = await _patientVitalRepository.GetPatientVitalAdded(nurseID);
                    if (patientVital == null)
                    {
                        TempData["Error"] = "Failed to retrieve patient vital.";
                        return View();
                    }

                    //Assign PatientVitalID
                    foreach (var item in vitals)
                    {
                        //at this point, item contains the vitalID and vitalValue

                        var patientVitalDetails = new PatientVitalDetails
                        {
                            VitalID = item.VitalID,
                            VitalValue = item.VitalValue,
                            PatientVitalsID = patientVital.PatientVitalsID
                        };

                        //Create Patient Vitals Details
                        var isAdded = await _patientVitalDetailsRepository.AddPatientVitalDetailsAsync(patientVitalDetails);
                        TempData["Success"] = "Vitals Added Successfully";
                    }

                }
                else
                {
                    TempData["Error"] = "Vitals not added";
                }


            }
            catch
            {
                TempData["Error"] = "Error: Contact Administrator";
            }
            // Prepare the data for each vital sign
            var heartRateOptions = GeneratePulseRateRange(40, 200, 10);
            var bloodPressureOptions = GenerateBloodPressureRange(90, 200, 10);
            var temperatureOptions = GenerateTemperatureRange(34.0, 41.0, 0.5);
            var respiratoryRateOptions = GenerateRespiratoryRateRange(10, 40, 2);
            var bloodSugarOptions = GenerateBloodSugarRange(60, 300, 10);

            // Pass the data to the view using ViewBag or ViewModel
            ViewBag.HeartRateOptions = new SelectList(heartRateOptions);
            ViewBag.BloodPressureOptions = new SelectList(bloodPressureOptions);
            ViewBag.TemperatureOptions = new SelectList(temperatureOptions);
            ViewBag.RespiratoryRateOptions = new SelectList(respiratoryRateOptions);
            ViewBag.BloodSugarOptions = new SelectList(bloodSugarOptions);


            return View();
        }
        [Authorize(1, 6, 7)]
        public async Task<IActionResult> PatientVitalsList()
        {
            var patientVitalsList = await _patientVitalRepository.GetAllPatientVitals();
            return View(patientVitalsList);


        }
        [Authorize(1, 6, 7)]
        //By FileID
        public async Task<IActionResult> ViewPatientVitalsDetails(int id)
        {

            var patientVitalsDetails = await _patientVitalDetailsRepository.GetPatientVitalDetailsByFileId(id);
            return View(patientVitalsDetails);
        }





        //trying something



        [Authorize(1, 6, 7)]

        public async Task<IActionResult> RecordNote(int id)
        {
            ViewBag.FileID=id;
            return View();
        }

        // POST method to add a new note. This handles form submission from the AddNote view.
        [HttpPost]
        public async Task<IActionResult> RecordNote(Note note, int id)
        {
            try
            {
                bool result = await _noteRepository.RecordNoteAsync(note); // Add note to the repository

                if (result)
                {
                    return RedirectToAction("RecordNote"); // Redirect to list notes if successful
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There was an error adding a note. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during note addition
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            ViewBag.FileID = id;

            // If adding the note fails, return the view with the current data
            return View();
        }






    }
}
