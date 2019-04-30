using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectCore.Logica.Models.ViewModel
{
    public class TasksIndexViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Is Completed")]
        public bool? IsCompleted { get; set; }

        [Display(Name = "Effort")]
        public int? Effort { get; set; }

        [Display(Name = "Remaining Work")]
        public int? RemainingWork { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }

        [Display(Name = "Priority")]
        public string Priority { get; set; }        
    }
}
