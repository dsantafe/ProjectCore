using System;
using System.Collections.Generic;

namespace ProjectCore.DAL.Models
{
    public partial class Artifacts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Projects Project { get; set; }
    }
}
