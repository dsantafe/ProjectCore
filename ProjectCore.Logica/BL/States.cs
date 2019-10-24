using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Logica.BL
{
    public class States
    {
        /// <summary>
        /// GET STATES
        /// </summary>
        /// <returns></returns>
        public List<Models.DB.States> GetStates()
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var listStates = (from _states in _context.States
                              where _states.Active == true
                              select new Models.DB.States
                              {
                                  Id = _states.Id,
                                  Name = _states.Name,
                                  Active = _states.Active
                              }).ToList();

            return listStates;
        }
    }
}
