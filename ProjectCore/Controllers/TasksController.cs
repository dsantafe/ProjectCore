using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index(int? projectId)
        {
            Logica.BL.Tasks tasks = new Logica.BL.Tasks();
            var listTasks = tasks.GetTasks(projectId, null);

            var listTasksViewModel = listTasks.Select(x => new Logica.Models.ViewModel.TasksIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Details = x.Details,
                ExpirationDate = x.ExpirationDate,
                IsCompleted = x.IsCompleted,
                Effort = x.Effort,
                RemainingWork = x.RemainingWork,
                State = x.States.Name,
                Activity = x.Activities.Name,
                Priority = x.Priorities.Name
            });

            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();
            ViewBag.Project = project;

            return View(listTasksViewModel);
        }

        public IActionResult Create(int? projectId)
        {
            var taskBindingModel = new Logica.Models.BindingModel.TasksCreateBindingModel
            {
                ProjectId = projectId
            };

            Logica.BL.Activities activities = new Logica.BL.Activities();
            ViewBag.Activities = activities.GetActivities();

            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.Priorities = priorities.GetPriorities();

            Logica.BL.States states = new Logica.BL.States();
            ViewBag.States = states.GetStates();

            return View(taskBindingModel);
        }

        [HttpPost]
        public IActionResult Create(Logica.Models.BindingModel.TasksCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                tasks.CreateTasks(model.Title,
                    model.Details,
                    model.ExpirationDate,
                    model.IsCompleted,
                    model.Effort,
                    model.RemainingWork,
                    model.StateId,
                    model.ActivityId,
                    model.PriorityId,
                    model.ProjectId);

                return RedirectToAction("Index", new { projectId = model.ProjectId });
            }

            Logica.BL.Activities activities = new Logica.BL.Activities();
            ViewBag.Activities = activities.GetActivities();

            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.Priorities = priorities.GetPriorities();

            Logica.BL.States states = new Logica.BL.States();
            ViewBag.States = states.GetStates();

            return View(model);
        }

        public IActionResult Calendar(int? projectId)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();

            ViewBag.Project = project;
            return View();
        }

        public IActionResult GetTasksCalendar(int? projectId)
        {
            try
            {
                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                var listTasks = tasks.GetTasks(projectId, null);

                var listTasksCalendarViewModel = listTasks.Select(x => new Logica.Models.ViewModel.TasksGetTasksCalendarViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    AllDay = true,
                    Color = "#FFFF00",
                    Start = x.ExpirationDate.Value.AddDays(Convert.ToDouble(-x.RemainingWork)).ToString("yyyy-MM-dd HH:mm:ss"),
                    End = x.ExpirationDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    TextColor = "#000000"
                }).ToList();

                return Json(new
                {
                    Data = listTasksCalendarViewModel,
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
    }



}



