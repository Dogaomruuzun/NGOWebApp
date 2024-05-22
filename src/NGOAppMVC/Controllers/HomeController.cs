using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.Areas.Identity.Data;
using NGOAppMVC.DBModels;
using NGOAppMVC.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.Json;

namespace NGOAppMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly DCodeNGOdataNGOsqliteContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            TempData.Clear();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel(); ;
            model = addListData(model);


            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel Model)
        {
            var mailCheck = _context.Ngouser.Where(u => u.Email == Model.Email).ToList();
            if (mailCheck.Count > 0)
            {
                ModelState.AddModelError("", "Email already exists.");
            }

            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher();
                string hashedPassword = passwordHasher.HashPassword(Model.Password);

                var UserId = _context.Ngouser.Max(u => u.Id) + 1;

                var AllUsers = _context.Ngouser.ToList();
                var ngouser = new Ngouser
                {
                    Id = UserId,
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Age = Model.Age,
                    Email = Model.Email,
                    PhoneNumber = Model.PhoneNumber,
                    PasswordHash = hashedPassword,
                    RegisteredDate = DateTime.Now.ToString(),
                    Gender = (bool)Model.Gender ? 1 : 0,
                    ApprovementStatusId = 1 
                };
                _context.Add(ngouser);

                var volunteerInfo = new Volunteer
                {
                    AnnualIncome = Model.VolunteerAnnualIncome,
                    AvailableDaysCount = Model.VolunteerAvailableDaysCount,
                    AvailableHoursCount = Model.VolunteerAvailableHoursCount,
                    CanHandleOwnTransportation = (bool)Model.VolunteerCanHandleOwnTransportation ? 1 : 0,
                    NgouserId = UserId,
                    ProfessionId = Model.VolunteerProfessionId,
                    RegionId = Model.VolunteerRegionId
                };
                _context.Add(volunteerInfo);

                var indigentInfo = new Indigent
                {
                    MonthlyIncome = Model.IndigentMonthlyIncome,
                    DonableItemId = Model.IndigentDonableId,
                    MonthlyExpenditures = Model.IndigentMonthlyExpenditures,
                    NgouserId = UserId,
                    RegionId = Model.IndigentRegionId
                };
                _context.Add(indigentInfo);

                if (TempData["Dependents"] != null)
                {
                    Model.Dependents = JsonSerializer.Deserialize<List<DTOIndigentDependents>>(TempData["Dependents"].ToString());
                }

                if (Model.Dependents != null)
                {
                    foreach (var item in Model.Dependents)
                    {
                        var indigentDependent = new IndigentDependents
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            EducationStatusId = item.EducationStatusId,
                            EmploymentStatusId = item.EmploymentStatusId,
                            DependentRelationId = item.DependentRelationId,
                            NgouserId = UserId
                        };
                        _context.Add(indigentDependent);
                    }
                }
                try
                {
                    _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException ex)
                {
                    // Handle database update exceptions
                    ModelState.AddModelError("", "An error occurred while updating the database. Please try again.");
                    // Log the error (uncomment ex variable name and write a log.)
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (DbEntityValidationException ex)
                {
                    // Handle validation exceptions
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", validationError.ErrorMessage);
                        }
                    }
                    // Log the error (uncomment ex variable name and write a log.)
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                    // Log the error (uncomment ex variable name and write a log.)
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Model);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddDependent(RegisterModel Model)
        {
            if (TempData["Dependents"] != null)
            {
                Model.Dependents = JsonSerializer.Deserialize<List<DTOIndigentDependents>>(TempData["Dependents"].ToString());
            }
            var newDependent = new DTOIndigentDependents
            {
                FirstName = Model.IndigentFirstName,
                LastName = Model.IndigentLastName,
                EducationStatusId = Model.IndigentEducationId,
                EducationStatusName = _context.LkpEducationalStatus.Where(e => e.Id == Model.IndigentEducationId).Select(s => s.Name).FirstOrDefault(),
                EmploymentStatusId = Model.IndigentEmploymentId,
                EmploymentStatusName = _context.LkpEmploymentStatus.Where(e => e.Id == Model.IndigentEmploymentId).Select(s => s.Name).FirstOrDefault(),
                DependentRelationId = Model.IndigentRelationId,
                DependentRelationName = _context.LkbDependentRelation.Where(e => e.Id == Model.IndigentRelationId).Select(s => s.Name).FirstOrDefault()
            };
            if (Model.Dependents == null)
            {
                Model.Dependents = new();
            }
            Model.Dependents.Add(newDependent);
            TempData["Dependents"] = JsonSerializer.Serialize(Model.Dependents);
            TempData.Keep("Dependents");
            Model = addListData(Model);
            return View("Register",Model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private RegisterModel addListData(RegisterModel model)
        {
            var educationStatusListDB = _context.LkpEducationalStatus.ToList();
            model.EducationList = new();
            if (educationStatusListDB?.Count > 0)
            {
                model.EducationList.Add(new SelectListItem { Text = "Education", Value = "0" });
                foreach (var item in educationStatusListDB)
                {
                    model.EducationList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }

            var professionListDB = _context.LkpProfession.ToList();
            model.ProfessionList = new();
            if (professionListDB?.Count > 0)
            {
                model.ProfessionList.Add(new SelectListItem { Text = "Profession", Value = "0" });
                foreach (var item in professionListDB)
                {
                    model.ProfessionList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }

            var regionsDB = _context.LkpRegions.ToList();
            model.GeographicalList = new();
            if (regionsDB?.Count > 0)
            {
                model.GeographicalList.Add(new SelectListItem { Text = "Region", Value = "0" });
                foreach (var item in regionsDB)
                {
                    var geoName = item.City + " - " + item.District + " - " + item.Neighborhood;
                    model.GeographicalList.Add(new SelectListItem { Text = geoName, Value = item.Id.ToString() });
                }
            }

            var relationsDB = _context.LkbDependentRelation.ToList();
            model.RelationList = new();
            if (relationsDB?.Count > 0)
            {
                model.RelationList.Add(new SelectListItem { Text = "Relation", Value = "0" });
                foreach (var item in relationsDB)
                {
                    model.RelationList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }

            var EmploymentsDB = _context.LkpEmploymentStatus.ToList();
            model.EmploymentStatusList = new();
            if (EmploymentsDB?.Count > 0)
            {
                model.EmploymentStatusList.Add(new SelectListItem { Text = "Employment", Value = "0" });
                foreach (var item in EmploymentsDB)
                {
                    model.EmploymentStatusList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            var DonableItemsDB = _context.LkpDonableItem.ToList();
            model.DonableItemList = new();
            if (DonableItemsDB?.Count > 0)
            {
                model.DonableItemList.Add(new SelectListItem { Text = "Donable", Value = "0" });
                foreach (var item in DonableItemsDB)
                {
                    model.DonableItemList.Add(new SelectListItem { Text = item.DonableTypeName, Value = item.Id.ToString() });
                }
            }
            return model;
        }
    }
}
