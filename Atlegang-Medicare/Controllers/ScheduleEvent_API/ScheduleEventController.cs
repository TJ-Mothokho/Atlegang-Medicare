using DataAccesslayer.Models.View_Models;
using DataAccesslayer.Repository.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlegang_Medicare.Controllers.ScheduleEvent_API
{
    // This class is an API controller, used for handling HTTP requests for scheduling events (visits).
    [Route("api/[controller]")] // Define the route template for this controller's actions. The [controller] placeholder will be replaced by the class name (without "Controller").
    [ApiController] // This attribute marks the class as an API controller, providing automatic model binding, validation, and other API-specific features.
    public class ScheduleEventController : ControllerBase // Inherit from ControllerBase instead of Controller, as no views are returned in API controllers.
    {
        // Declare a private read-only field for the IVisitRepository, which handles the data access logic for visits.
        private readonly IVisitRepository _visitRepository;

        // Constructor to inject the IVisitRepository via dependency injection.
        public ScheduleEventController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository; // Initialize the _visitRepository field with the injected repository.
        }

        // This method responds to GET requests and returns a list of visits in the form of ViewModel objects (VMVisit).
        // GET: api/ScheduleEvent (default route, due to Index method)
        [HttpGet] // Defines this action as handling HTTP GET requests.
        public async Task<List<VMVisit>> Index() // Asynchronous method returning a list of VMVisit objects.
        {
            // Initialize an empty list of VMVisit, which will be used to store the transformed visit data.
            List<VMVisit> vmVisits = new List<VMVisit>();

            // Asynchronously retrieve the list of visits from the repository.
            var visits = await _visitRepository.GetAllVisitsAsync();

            // Loop through each visit to map its properties into the corresponding ViewModel (VMVisit).
            foreach (var visit in visits)
            {
                // Create a new VMVisit object and map the properties from the Visit entity.
                VMVisit vmVisits1 = new VMVisit();
                vmVisits1.id = visit.VisitID; // Map the VisitID from the database entity to the ViewModel.
                vmVisits1.title = visit.Title; // Map the Title of the visit.
                vmVisits1.description = visit.Description; // Map the Description of the visit.

                // Format the StartDate and EndDate to ISO 8601 format (yyyy-MM-ddTHH:mm:ss) for compatibility with front-end tools like fullCalendar.
                vmVisits1.start = visit.StartDate.ToString("yyyy-MM-ddTHH:mm:ss");
                vmVisits1.end = visit.EndDate.ToString("yyyy-MM-ddTHH:mm:ss");

                // Add the newly created ViewModel object to the list.
                vmVisits.Add(vmVisits1);
            }

            // Return the list of transformed visit data as the response.
            return vmVisits;
        }
    }
}
