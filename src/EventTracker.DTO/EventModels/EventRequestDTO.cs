using EventTracker.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.EventModels
{
    public class EventRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (StartDate <= DateTime.Now)
            {
                yield return new ValidationResult("Start Date must be greater than today", new[] { nameof(StartDate) });
            }

            if (EndDate <= StartDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date", new[] { nameof(EndDate) });
            }
            
        }
    }
}
