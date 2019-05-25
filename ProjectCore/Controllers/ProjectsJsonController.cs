using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectCore.Controllers
{
    [Authorize]
    public class ProjectsJsonController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsJsonController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProjects()
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

                Logica.BL.Projects projects = new Logica.BL.Projects();
                var result = await _userManager.IsInRoleAsync(user, "Admin") ?
                    projects.GetProjects(null, tenant.Id) :
                    projects.GetProjects(null, tenant.Id, user.Id);

                var listProjects = result.Select(x => new Logica.Models.ViewModel.ProjectsIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Details = x.Details,
                    CreatedAt = x.CreatedAt,
                    ExpectedCompletionDate = x.ExpectedCompletionDate,
                    UpdateAt = x.UpdatedAt
                });

                listProjects = tenant.Plan.Equals("premium") ?
                    listProjects :
                    listProjects.Take(1).ToList();

                return Json(new
                {
                    Data = listProjects,
                    IsSuccessful = true
                });
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModel.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Logica.Models.BindingModel.ProjectsCreateBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                    var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

                    Logica.BL.Projects projects = new Logica.BL.Projects();
                    projects.CreateProjects(model.Title,
                        model.Details,
                        model.ExpectedCompletionDate,
                        tenant.Id);

                    return RedirectToAction("Index");
                }

                return Json(new
                {
                    IsSuccessful = true
                });
            }
            catch (Exception ex)
            {
                return Json(new Logica.Models.ViewModel.ResponseViewModel
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        public IActionResult Edit(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(id, null).FirstOrDefault();

            var projectBindingModel = new Logica.Models.BindingModel.ProjectsEditBindingModel
            {
                Id = project.Id,
                Details = project.Details,
                Title = project.Title,
                ExpectedCompletionDate = project.ExpectedCompletionDate
            };

            return View(projectBindingModel);
        }

        [HttpPost]
        public IActionResult Edit(Logica.Models.BindingModel.ProjectsEditBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Logica.BL.Projects projects = new Logica.BL.Projects();
                projects.UpdateProjects(model.Id,
                    model.Title,
                    model.Details,
                    model.ExpectedCompletionDate);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(id, null).FirstOrDefault();

            var projectViewModel = new Logica.Models.ViewModel.ProjectsDetailsViewModel
            {
                Id = project.Id,
                Details = project.Details,
                Title = project.Title,
                ExpectedCompletionDate = project.ExpectedCompletionDate,
                CreatedAt = project.CreatedAt,
                UpdateAt = project.UpdatedAt
            };

            return View(projectViewModel);
        }

        public IActionResult Delete(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            projects.DeleteProjects(id);

            return RedirectToAction("Index");
            //return RedirectToAction("Index","Projects");
            //puedo redireccionar a acciones de otros controladores
        }
    }
}