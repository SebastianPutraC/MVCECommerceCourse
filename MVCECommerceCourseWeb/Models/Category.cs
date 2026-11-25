using System.ComponentModel.DataAnnotations;

namespace MVCECommerceCourseWeb.Models
{
    public class Category
    {
        //[Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        //Error check for wrong input on create view
        [Range(0,100, ErrorMessage = "Range must be between 0 and 100!!")]
        public int DisplayOrder { get; set; }
    }
}
