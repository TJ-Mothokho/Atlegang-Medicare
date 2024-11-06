using DataAccesslayer.Models.Domains.Administrator;
using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Services;
using DataAccesslayer.Repository.Administrator;
using Microsoft.AspNetCore.Mvc.Rendering;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;
using Atlegang_Medicare.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Atlegang_Medicare.Controllers.Administrator
{

    public class AdministratorController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISuburbRepository _suburbRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IAdministratorDashboardCardsRepository _administratorDashboardCardsRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IBedRepository _bedRepository;

        public AdministratorController(IBedRepository bedRepository,IWardRepository wardRepository, IUserRepository userRepository, IRoleRepository roleRepository, ISuburbRepository suburbRepository, ICityRepository cityRepository, IAdministratorDashboardCardsRepository administratorDashboardCardsRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _suburbRepository = suburbRepository;
            _roleRepository = roleRepository;
            _administratorDashboardCardsRepository = administratorDashboardCardsRepository;
            _wardRepository = wardRepository;
            _bedRepository = bedRepository;
        }
        [Authorize(1)]
        public async Task<IActionResult> Index()
        {
            ViewBag.EmployeeList = await _userRepository.GetAllEmployeesAsync();
            var administratorDashboardCards = await _administratorDashboardCardsRepository.GetAdministratorDashboardCardsAsync();
            return View(administratorDashboardCards);
        }
        [Authorize(1)]
        public async Task<IActionResult> AddBed()
        {
            ViewBag.WardNames = await GetWardNamesDropDownList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBed(Ward ward)
        {
            try
            {
                bool result = await _bedRepository.AddBedAsync(ward.WardID);

                if (result)
                {
                    TempData["SuccessBed"] = "Bed Added ";
                }
                else
                {
                    TempData["ErrorBed"] = "Failed to add a bed, please try again";
                }

            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [Authorize(1)]
        public async Task<IActionResult> ListBeds()
        {
            var bedList = await _bedRepository.GetAllBedsAsync();
            return View(bedList);
        }
        [Authorize(1)]
        public async Task<IActionResult> DeleteBed(int id)
        {
            var bed = await _bedRepository.GetBedByIdAsync(id);
            return View(bed);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteBed(Bed bed)
        {
            try
            {
                bool result = await _bedRepository.DeleteBedAsync(bed.BedID);

                if (result)
                {
                    TempData["SuccessRemoved"] = "Bed Deleted ";
                }
                else
                {
                    TempData["ErrorBed1"] = "Failed to DELETE a bed, please try again";
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View();
        }

        [Authorize(1)]
        public async Task<IActionResult> AddEmployee()
        {
            PasswordGenerator password = new PasswordGenerator();
            ViewBag.GeneratePassword = password.GetPassword();
           
            ViewBag.RoleList = await GetRolesDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.CityList = await GetCitiesDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();

            return View();
        }

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

        private async Task<IEnumerable<SelectListItem>> GetWardNamesDropDownList()
        {
            var wardNames = await _wardRepository.GetAllWardsAsync();

            return wardNames.Select(wardName => new SelectListItem
            {
                Value = wardName.WardID.ToString(),
                Text = wardName.Name
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetRolesDropDownList()
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            return roles.Select(role => new SelectListItem
            {
                Value = role.RoleID.ToString(),
                Text = role.RoleName
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetSuburbsDropDownList()
        {
            var suburbs = await _suburbRepository.GetAllSuburbsAsync();

            return suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbID.ToString(),
                Text = suburb.SuburbName
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetCitiesDropDownList()
        {
            var cities = await _cityRepository.GetAllCitiesAsync();

            return cities.Select(city => new SelectListItem
            {
                Value = city.CityID.ToString(),
                Text = city.CityName
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(User user)
        {
            // Create an instance of the PasswordHasher class
            PasswordHasher handler = new PasswordHasher();


            // Hash the password
            user.Password = handler.HashPassword(user.Password);


            try
            {
                bool result = await _userRepository.AddEmployeeAsync(user);

                if (result)
                {
                    TempData["Success"] = "Employee Added ";
                }
                else
                {
                    TempData["Error"] = "Failed to add an employee, please try again";
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View();
        }

        [Authorize(1,3)]
        public async Task<IActionResult> ViewEmployee(int id)
        { 
            var employee = await _userRepository.GetUserByIdAsync(id);
            return View(employee);
        }
        [Authorize(1)]
        public async Task<IActionResult> UpdateEmployee(int id)
        {

            PasswordGenerator password = new PasswordGenerator();
            ViewBag.GeneratePassword = password.GetPassword();
            ViewBag.RoleList = await GetRolesDropDownList();
            ViewBag.SuburbList = await GetSuburbsDropDownList();
            ViewBag.CityList = await GetCitiesDropDownList();
            ViewBag.GenderList = GetGenderDropDownList();
            var employee = await _userRepository.GetUserByIdAsync(id);
           

            return View(employee);
        }
      
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(User user)
        {

            try
            {
                bool result = await _userRepository.UpdateEmployeeAsync(user);

                if (result)
                {
                    TempData["Success"] = "Employee Updated ";
                }
                else
                {
                    TempData["Error"] = "Failed to update an employee details, please try again";
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }

            return View();
        }
        [Authorize(1)]
        public async Task<IActionResult> ListEmployee()
        {
            var employees = await _userRepository.GetAllEmployeesAsync();
            return View(employees);
        }
       
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _userRepository.GetUserByIdAsync(id);
            return View(employee);
        }
        [Authorize(1)]
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(User user)
        {

            try
            {
                bool result = await _userRepository.DeleteUserAsync(user.UserID);

                if (result)
                {
                    return RedirectToAction("ListEmployee");
                }

            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            }
            return RedirectToAction("ListEmployee");
        }
        [Authorize(1)]
        public async Task<IActionResult> Report()
        {


            DateTime dateStart = DateTime.Now.AddYears(-1);
            DateTime dateEnd = DateTime.Now;

            
                
                // Create a list of dummy report data
            var reports = new List<Report>
        {
            new Report
            {
                ReportID = 1,
                Subject = "Monthly Review",
                DateOccured = new DateTime(2024, 1, 15),
                Type = "Review"
            },
            new Report
            {
                ReportID = 2,
                Subject = "Weekly Update",
                DateOccured = new DateTime(2024, 2, 5),
                Type = "Update"
            },
            new Report
            {
                ReportID = 3,
                Subject = "Quarterly Assessment",
                DateOccured = new DateTime(2024, 3, 20),
                Type = "Assessment"
            },
            new Report
            {
                ReportID = 4,
                Subject = "Quarterly Assessment",
                DateOccured = new DateTime(2024, 3, 20),
                Type = "Assessment"
            }
        };

            // Filter the reports based on the date range
            var filteredReports = reports
                .Where(r => r.DateOccured >= dateStart && r.DateOccured <= dateEnd)
                .ToList();

            // Pass the filtered list to the view
            return View(filteredReports);
        }


    }
}
