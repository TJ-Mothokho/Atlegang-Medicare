using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atlegang_Medicare.Controllers.Doctor
{
    public class EventVisitController : Controller
    {
        // Declare private read-only fields for dependency injection of repositories.
        private readonly IPatientFileRepository _patientFileRepository; // Interface for accessing patient file data.
        private readonly IVisitRepository _visitRepository; // Interface for accessing visit data.

        // Constructor to inject the required repositories via dependency injection.
        public EventVisitController(IPatientFileRepository patientFileRepository, IVisitRepository visitRepository)
        {
            _patientFileRepository = patientFileRepository; // Initialize the _patientFileRepository field.
            _visitRepository = visitRepository; // Initialize the _visitRepository field.
        }
        [Authorize(1,2,3,6,7)]
        // Action method to display the default page for managing visits.
        public async Task<IActionResult> Index()
        {
            // Populate the dropdown for visit types and assign to ViewBag for use in the view.
            ViewBag.VisitTitle = GetVisitTypeDropDownList();

            // Asynchronously load the patient files for the dropdown and assign to ViewBag.
            ViewBag.PatientName = await GetPatientFileDropDownList();
            return View(); // Simply returns the view for the index page without any data for now.
        }

        [Authorize(1,2)]
        // Asynchronous method to load the AddVisit form, including dropdown lists.
        public async Task<IActionResult> AddVisit()
        {
            // Populate the dropdown for visit types and assign to ViewBag for use in the view.
            ViewBag.VisitTitle = GetVisitTypeDropDownList();

            // Asynchronously load the patient files for the dropdown and assign to ViewBag.
            ViewBag.PatientName = await GetPatientFileDropDownList();

            // Return the AddVisit view for the user to input visit details.
            return View();
        }

        // POST method to handle the form submission when adding a new visit.
        [HttpPost] // Specifies that this method responds to POST requests.
        public async Task<IActionResult> AddVisit(Visit visit) // Receives the visit object populated with form data.
        {
            try
            {
                // Set the DoctorID for the visit using the current user's ID from the session.
                visit.DoctorID = HttpContext.Session.GetInt32("UserID").Value;

                // Use the repository to asynchronously add the new visit to the database.
                bool result = await _visitRepository.AddVisitAsync(visit);

                // If the visit was successfully added, show a success message.
                if (result)
                {
                    TempData["SuccessAlert"] = "Appointment Scheduled ";
                }
                else
                {
                    // If adding the visit fails (e.g., invalid data), show an error message.
                    TempData["ErrorAlert"] = "Failed to add appointment, please try again";
                    // Populate the dropdown for visit types and assign to ViewBag for use in the view.
                    ViewBag.VisitTitle = GetVisitTypeDropDownList();

                    // Asynchronously load the patient files for the dropdown and assign to ViewBag.
                    ViewBag.PatientName = await GetPatientFileDropDownList();

                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions by adding a model error to display to the user.
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            // Return the same view with the visit data, useful if form submission failed.
            return View(visit);
        }

        // Helper method to generate a list of visit types for a dropdown, returning IEnumerable<SelectListItem>.
        private IEnumerable<SelectListItem> GetVisitTypeDropDownList()
        {
            // Define a list of available visit types.
            List<string> visitTypes = new List<string>
            {
                "Initial Visit",
                "Follow Up Visit",
                "Emergency Visit"
            };

            // Convert the list of strings into SelectListItem objects for the dropdown.
            return visitTypes.Select(visitType => new SelectListItem
            {
                Value = visitType, // The actual value of the dropdown item.
                Text = visitType // The text that will be displayed to the user.
            });
        }

        // Asynchronous helper method to fetch all patient files for a dropdown list.
        public async Task<IEnumerable<SelectListItem>> GetPatientFileDropDownList()
        {
            // Retrieve the patient files from the repository.
            var patientFiles = await _patientFileRepository.GetAllPatientFilesForVisit();

            // Convert the patient files into SelectListItem objects for the dropdown.
            return patientFiles.Select(patientFile => new SelectListItem
            {
                Value = patientFile.FileID.ToString(), // Use FileID as the value.
                Text = patientFile.PatientName // Display the patient's name.
            });
        }


        [Authorize(1, 2)]
        // Asynchronous method to load the UpdateVisit form, pre-populated with the visit's current data.
        public async Task<IActionResult> UpdateVisit(int id)
        {
            // Load the dropdown for visit types.
            ViewBag.VisitTitle = GetVisitTypeDropDownList();

            // Fetch the appointment data by its ID.
            var appointment = await _visitRepository.GetVisitByIdAsync(id);

            // Return the view with the current appointment data for updating.
            return View(appointment);
        }

        // POST method to handle the form submission when updating a visit.
        [HttpPost] // Specifies that this method responds to POST requests.
        public async Task<IActionResult> UpdateVisit(Visit visit) // Receives the updated visit object from the form.
        {
            try
            {
                // Attempt to update the visit data in the repository.
                bool result = await _visitRepository.UpdateVisitAsync(visit);

                // If successful, show a success message.
                if (result)
                {
                    TempData["SuccessUpdate"] = "Appointment updated ";
                }
                else
                {
                    // If the update fails, show an error message.
                    TempData["ErrorUpdate"] = "Failed to update appointment, please try again";
                }
            }
            catch
            {
                // Handle any errors by adding a model error.
                ModelState.AddModelError(string.Empty, "There was an error updating appointment. Please try again.");
            }

            // Return the same view (UpdateVisit) in case of errors.
            return View();
        }


        [Authorize(1, 2)]
        // Asynchronous method to list all visits.
        public async Task<IActionResult> ViewVisit(int id)
        {
            // Fetch the visit by its ID from the repository.
            var appointment = await _visitRepository.GetVisitByIdAsync(id);

            ViewBag.PatientVisits = await _visitRepository.GetVisitByFileIdAsync(appointment.FileID);

            // Return the view with the appointment data.
            return View(appointment);
        }
        [Authorize(1, 2)]
        public async Task<IActionResult> ListVisit()
        {
            var visits = await _visitRepository.GetAllVisitsAsync();
            return View(visits);
        }        
    }
}
