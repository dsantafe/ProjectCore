using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProjectCore.Logica.Models.BindingModel
{
    public class TasksCreateBindingModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field Title is required")]
        public string Title { get; set; }

        [Display(Name = "Details")]
        [Required(ErrorMessage = "The field Details is required")]
        public string Details { get; set; }

        [Display(Name = "Expiration Date")]
        [Required(ErrorMessage = "The field Expiration Date is required")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Is Completed")]
        [Required(ErrorMessage = "The field Is Completed is required")]
        public bool IsCompleted { get; set; }

        [Display(Name = "Effort")]
        [Required(ErrorMessage = "The field Effort is required")]
        public int? Effort { get; set; }

        [Display(Name = "Remaining Work")]
        [Required(ErrorMessage = "The field Remaining Work is required")]
        public int? RemainingWork { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "The field State is required")]
        public int? StateId { get; set; }

        [Display(Name = "Activity")]
        [Required(ErrorMessage = "The field Activity is required")]
        public int? ActivityId { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "The field Priority is required")]
        public int? PriorityId { get; set; }
        
        public int? ProjectId { get; set; }
    }
}
