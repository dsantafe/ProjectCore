using System;

namespace ProjectCore.Logica.Models.DB
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsCompleted { get; set; }
        public int? Effort { get; set; }
        public int? RemainingWork { get; set; }

        //FOREIGN KEY
        public int? StateId { get; set; }
        public int? ActivityId { get; set; }
        public int? PriorityId { get; set; }
        public int? ProjectId { get; set; }

        public Activities Activities { get; set; }
        public Priorities Priorities { get; set; }
        public Projects Projects { get; set; }
        public States States { get; set; }
    }
}
