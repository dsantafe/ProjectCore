using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Logica.BL
{
    public class Projects
    {
        /// <summary>
        /// GET PROJECTS BY ID OR TENANT OR USER PROJECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Models.DB.Projects> GetProjects(int? id,
            int? tenantId,
            string userId = null)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var listProjectsEF = (from _projects in _context.Projects
                                  select _projects).ToList();

            if (id != null)
                listProjectsEF = listProjectsEF.Where(x => x.Id == id).ToList();
            if (tenantId != null)
                listProjectsEF = listProjectsEF.Where(x => x.TenantId == tenantId).ToList();
            if (!string.IsNullOrEmpty(userId))
                listProjectsEF = (from _projects in listProjectsEF
                                  join _userProjects in _context.UserProjects on _projects.Id equals _userProjects.ProjectId
                                  where _userProjects.UserId.Equals(userId)
                                  select _projects).ToList();

            var listProjects = (from _projects in listProjectsEF
                                select new Models.DB.Projects
                                {
                                    Id = _projects.Id,
                                    Title = _projects.Title,
                                    Details = _projects.Details,
                                    ExpectedCompletionDate = _projects.ExpectedCompletionDate,
                                    TenantId = _projects.TenantId,
                                    CreatedAt = _projects.CreatedAt,
                                    UpdatedAt = _projects.UpdatedAt
                                }).ToList();

            return listProjects;
        }

        /// <summary>
        /// CREATE PROJECTS
        /// </summary>
        /// <param name="title"></param>
        /// <param name="details"></param>
        /// <param name="expectedCompletionDate"></param>
        /// <param name="tenantId"></param>
        public void CreateProjects(string title,
            string details,
            DateTime? expectedCompletionDate,
            int? tenantId)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            _context.Projects.Add(new DAL.Models.Projects
            {
                Title = title,
                Details = details,
                ExpectedCompletionDate = expectedCompletionDate,
                TenantId = tenantId,
                CreatedAt = DateTime.Now
            });

            //INSERT INTO Projects(Title, Details, ExpectedCompletionDate, TenantId, CreatedAt)
            //VALUES('','','YYYY-MM-DD', 1, 'YYYY-MM-DD')

            //aplica todos los cambios detectados a nivel de objetos en la bd
            _context.SaveChanges();
        }

        /// <summary>
        /// UPDATE PROJECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="details"></param>
        /// <param name="expectedCompletionDate"></param>
        public void UpdateProjects(int id,
           string title,
           string details,
           DateTime? expectedCompletionDate)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var projectEF = _context.Projects.Where(x => x.Id == id).FirstOrDefault();

            //(from _projects in _context.Projects
            // where _projects.Id == id
            // select _projects).FirstOrDefault();

            projectEF.Title = title;
            projectEF.Details = details;
            projectEF.ExpectedCompletionDate = expectedCompletionDate;
            projectEF.UpdatedAt = DateTime.Now;

            //UPDATE Projects 
            //SET Title = '', Details = '', ExpectedCompletionDate = '', UpdatedAt = '')
            //WHERE Id = x

            //aplica todos los cambios detectados a nivel de objetos en la bd
            _context.SaveChanges();
        }

        /// <summary>
        /// DELETE PROJECTS
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProjects(int? id)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            //validamos dependencias de la tabla projects
            if (_context.Artifacts.Any(x => x.ProjectId == id) || _context.UserProjects.Any(x => x.ProjectId == id))
                return;

            var projectEF = _context.Projects.Where(x => x.Id == id).FirstOrDefault();

            _context.Projects.Remove(projectEF);

            //DELETE FROM Projects 
            //WHERE Id = X

            //aplica todos los cambios detectados a nivel de objetos en la bd
            _context.SaveChanges();
        }
    }
}
