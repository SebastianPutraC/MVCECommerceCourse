using System.ComponentModel.DataAnnotations;

namespace MVCECommerceCourseWeb.Models
{
    public class Category
    {
        [Key]
        public int CatgeoryIndentification { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
