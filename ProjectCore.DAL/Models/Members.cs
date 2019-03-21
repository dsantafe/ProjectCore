using System;
using System.Collections.Generic;

namespace ProjectCore.DAL.Models
{
    public partial class Members
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public AspNetUsers User { get; set; }
    }
}
