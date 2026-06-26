using System.ComponentModel.DataAnnotations;

namespace WorkAd.Data
{
    public class WorkAdvert
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Hourly rate in zł is required")]
        [Range(31.40, 999.99, ErrorMessage = "Hourly rate must be heigher than 31.40 zł and lower than 999.99 zł ")]
        public decimal HourlyRate { get; set; }
        [Required(ErrorMessage = "Contract type is required")]
        public ContractType ContractType { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;
        [Required(ErrorMessage = "Work type is required")]
        public WorkType WorkType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}