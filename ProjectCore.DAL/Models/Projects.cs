using System;
using System.Collections.Generic;

namespace ProjectCore.DAL.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Artifacts = new HashSet<Artifacts>();
            UserProjects = new HashSet<UserProjects>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public int? TenantId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Tenants Tenant { get; set; }
        public ICollection<Artifacts> Artifacts { get; set; }
        public ICollection<UserProjects> UserProjects { get; set; }
    }
}
