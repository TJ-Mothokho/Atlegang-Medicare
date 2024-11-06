using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.Domains.Script_Manager;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atlegang_Medicare.Controllers.Script_Manager
{
    [Authorize(1, 5)]
    public class ScriptManagerController : Controller
    {
        public List<ScriptDetail> scriptDetails = new List<ScriptDetail>();
        private readonly IScriptRepository _scriptRepository;
        private readonly IScriptDetailRepository _scriptDetailRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public ScriptManagerController(IHttpContextAccessor contextAccessor, IScriptRepository scriptRepository, IScriptDetailRepository scriptDetailRepository, IMedicationRepository medicationRepository)
        {
            _scriptDetailRepository = scriptDetailRepository;
            _medicationRepository = medicationRepository;
            _scriptRepository = scriptRepository;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            // Get all necessary data
            var scripts = await _scriptRepository.GetAllScriptsAsync();
            int managerID = _contextAccessor.HttpContext.Session.GetInt32("UserID").Value;

            ViewBag.pendingScripts = (from script in scripts
                                 where script.Status == "Pending"
                                 select script.ScriptID).Count();

            ViewBag.approvedScripts = (from script in scripts
                                  where script.Status == "Approved" && script.ManagerID == managerID
                                   select script.ScriptID).Count();

            ViewBag.declinedScripts = (from script in scripts
                                  where script.Status == "Declined" && script.ManagerID == managerID
                                  select script.ScriptID).Count();


            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            // Get all necessary data
            var scripts = await _scriptRepository.GetAllScriptsAsync();
            int managerID = _contextAccessor.HttpContext.Session.GetInt32("UserID").Value;

            int approvedScripts = (from script in scripts
                                       where script.Status == "Approved" && script.ManagerID == managerID
                                       select script.ScriptID).Count();

            int declinedScripts = (from script in scripts
                                       where script.Status == "Declined" && script.ManagerID == managerID
                                       select script.ScriptID).Count();

            var result = new
            {
                approvedScripts,
                declinedScripts
            };

            return Json(result);
        }

        [Route("/[Controller]/Scripts")]
        public async Task<IActionResult> ViewScripts()
        {
            ViewBag.Scripts = await _scriptRepository.GetAllScriptsAsync();

            return View();
        }

        [Route("/[Controller]/Details")]
        public async Task<IActionResult> ScriptDetails(int scriptID)
        {
            var scripts = await _scriptRepository.GetAllScriptsAsync();
            ViewBag.ScriptDetails = await _scriptDetailRepository.GetScriptDetailsByScriptIdAsync(scriptID);

            var scriptRow = from item in scripts
                         where item.ScriptID == scriptID
                         select new { item.Manager, item.Status };

            var script = scriptRow.FirstOrDefault();
            ViewBag.Manager = script.Manager;
            ViewBag.Status = script.Status;

            return View();
        }

        public async Task<IActionResult> VerifyScript(int scriptID, int managerID, string status)
        {
            var script = new Script
            {
                ScriptID = scriptID,
                ManagerID = managerID,
                Status = status
            };

            if (await _scriptRepository.UpdateScriptAsync(script))
            {
                if (script.Status == "Approved")
                {
                    TempData["Success"] = "Prescription Approved";
                }
                else if (script.Status == "Declined")
                {
                    TempData["Error"] = "Prescription Declined";
                }
            }

            return RedirectToAction("ViewScripts");
        }


        [Route("/Medication")]
        public async Task<IActionResult> ViewMedication()
        {
            var medication = await _medicationRepository.GetAllMedicationsAsync();

            return View(medication);
        }

        [Route("/Medication/Add")]
        public IActionResult AddMedication()
        {
            return View();
        }

        [HttpPost]
        [Route("/Medication/Add")]
        public async Task<IActionResult> AddMedication(Medication medication)
        {
            if (await _medicationRepository.AddMedicationAsync(medication))
            {
                TempData["Success"] = "Medication Added Successfully";

                return RedirectToAction("ViewMedication");
            }

            TempData["Error"] = "Medication Not Added";

            return View(medication);
        }

        public async Task<IActionResult> DeleteMedication(int medicationID)
        {
            if (await _medicationRepository.DeleteMedicationAsync(medicationID))
            {
                TempData["Success"] = "Medication Removed!";

                return RedirectToAction("ViewMedication");
            }
            TempData["Error"] = "Medication Not Removed!";

            return RedirectToAction("ViewMedication");
        }

        [Route("/Medication/Edit")]
        public async Task<IActionResult> EditMedication(int medicationID)
        {
            var medication = await _medicationRepository.GetMedicationByIdAsync(medicationID);

            return View(medication);
        }

        [HttpPost]
        [Route("/Medication/Edit")]
        public async Task<IActionResult> EditMedication(Medication medication)
        {
            if (await _medicationRepository.UpdateMedicationAsync(medication))
            {
                TempData["Success"] = "Medication Updated!";

                return RedirectToAction("ViewMedication");
            }
            TempData["Error"] = "Medication Not Updated!";

            return View(medication);
        }

        //To clear my TempData everytime i run a new action
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            TempData.Clear();
        }
    }
}
