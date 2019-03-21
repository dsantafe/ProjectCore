using System;
using System.Collections.Generic;

namespace ProjectCore.DAL.Models
{
    public partial class Tenants
    {
        public Tenants()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Plan { get; set; }

        public ICollection<AspNetUsers> AspNetUsers { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
