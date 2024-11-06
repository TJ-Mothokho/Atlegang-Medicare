using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccesslayer.Models.View_Models.Administrator;
using System;
using DataAccesslayer.Models.Domains.Doctor;
using Microsoft.AspNetCore.Http;

namespace Atlegang_Medicare.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISuburbRepository _suburbRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IBusinessInformationRepository _businessInformation;



        public AccountController(IUserRepository userRepository, ISuburbRepository suburbRepository, IWebHostEnvironment environment, IBusinessInformationRepository businessInformation)
        {
            _userRepository = userRepository;
            _suburbRepository = suburbRepository;
            _environment = environment;
            _businessInformation = businessInformation;
        }

        public async Task<IActionResult> LandingPage()
        {
            await GetSessionRequest();

            return View();
        }

        [Route("/login")]
        public async Task<IActionResult> LoginPage()
        {
            return View();
        }

        // Login action that handles form submission
        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> LoginPage(string email, string password)
        {
            // Create an instance of the PasswordHasher class
            PasswordHasher handler = new PasswordHasher();

            // Hash the password with the salt
            string hashedPassword = handler.HashPassword(password);


            //After user logins in with email & password, it goes to the database and checks if the user exists
            //It will store the user information in the "user" variable (or null if user is not found)
            var user = await _userRepository.Login(email, hashedPassword);

            //If the user is found/credentials are correct
            if (user == null)
            {
                //user not found/invalid credentials
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return View();
            }
            else
            {
                // Store user ID, RoleID & UserName in session
                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetInt32("RoleID", user.RoleID);
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);

                // Redirect to the Index action of the Home controller
                return RedirectToAction("Home", "Account", user);
            }
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            //Clear session cookies (this includes user information)
            await GetSessionRequest();

            return RedirectToAction("LoginPage");
        }

        public async Task<IActionResult> Home(User user)
        {
            await GetSessionRequest();


            if (user.RoleID == 1)
            {
                return RedirectToAction("Index", "Administrator");
            }
            else if (user.RoleID ==2)
            {
                return RedirectToAction("Index", "Doctor");
            }
            else if (user.RoleID == 3)
            {
                return RedirectToAction("Index", "WardAdministrator");
            }
            else if (user.RoleID == 4)
            {
                return RedirectToAction("Index", "ConsumableManager");
            }
            else if (user.RoleID == 5)
            {
                return RedirectToAction("Index", "ScriptManager");
            }
            else if (user.RoleID == 6 || user.RoleID == 7)
            {
                return RedirectToAction("Index", "Nurse");
            }
            
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            await GetSessionRequest();
            return View();
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            List<string> genders = new List<string>
            {
                "Male",
                "Female",
                "Other"
            };

            var suburbs = await _suburbRepository.GetAllSuburbsAsync();

            ViewBag.SuburbList = new SelectList(suburbs, "SuburbID", "SuburbName");
            ViewBag.GenderList = new SelectList(genders);

            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var user = await _userRepository.GetUserDetails(userID);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Profile(VMUser vMUser)
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var user = await _userRepository.GetUserDetails(userID);
            if (user == null)
            {
                return NotFound();
            }

            // update the image file if we have a new image file
            string newFileName = user.ImageFile.ToString();
            if (vMUser.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(vMUser.ImageFile!.FileName);

                string imageFullPath = _environment.WebRootPath + "/Profile/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    vMUser.ImageFile.CopyTo(stream);
                }

                // delete the old image
                string oldImageFullPath = _environment.WebRootPath + "/Profile/" + user.ImageFile;
                System.IO.File.Delete(oldImageFullPath);
            }

            //Update the user profile
            user.FirstName = vMUser.FirstName;
            user.LastName = vMUser.LastName;
            user.PhoneNumbers = vMUser.PhoneNumbers;
            user.SuburbID = vMUser.SuburbID;
            //user.ImageFile = newFileName.;
            user.ResidentialAddress = vMUser.ResidentialAddress;
            user.Gender = vMUser.Gender;

            return View(user);
        }

        private async Task GetSessionRequest()
        {
            var businessInfo = await _businessInformation.GetBusinessInformationAsync();
            //content
            HttpContext.Session.SetString("BusinessName", businessInfo.BusinessName);
            HttpContext.Session.SetString("Slogan", businessInfo.Slogan);
            HttpContext.Session.SetString("MainLogo", businessInfo.MainLogo);
            HttpContext.Session.SetString("FooterLogo", businessInfo.FooterLogo);
            HttpContext.Session.SetString("Facebook", businessInfo.FacebookLink);
            HttpContext.Session.SetString("Instagram", businessInfo.InstagramLink);
            HttpContext.Session.SetString("YouTube", businessInfo.YouTubeLink);
            HttpContext.Session.SetString("Twitter", businessInfo.TwitterLink);
        }


    }
}
