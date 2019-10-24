using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Logica.BL
{
    public class Activities
    {
        /// <summary>
        /// GET ACTIVITIES
        /// </summary>
        /// <returns></returns>
        public List<Models.DB.Activities> GetActivities()
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var listActivities = (from _activities in _context.Activities
                                  where _activities.Active == true
                                  select new Models.DB.Activities
                                  {
                                      Id = _activities.Id,
                                      Name = _activities.Name,
                                      Active = _activities.Active
                                  }).ToList();

            return listActivities;
        }
    }
}
