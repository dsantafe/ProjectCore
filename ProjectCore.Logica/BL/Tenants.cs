using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Logica.BL
{
    public class Tenants
    {
        /// <summary>
        /// GET TENANTS BY USER 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Models.DB.Tenants> GetTenants(string userId)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var listTenants = (from _tenants in _context.Tenants
                               join _aspNetUsers in _context.AspNetUsers on _tenants.Id equals _aspNetUsers.TenantId
                               where _aspNetUsers.Id.Equals(userId)
                               select new Models.DB.Tenants
                               {
                                   Id = _tenants.Id,
                                   Name = _tenants.Name,
                                   Plan = _tenants.Plan,
                                   CreatedAt = _tenants.CreatedAt,
                                   UpdatedAt = _tenants.UpdatedAt
                               }).ToList();

            return listTenants;
        }
    }
}
