using System.ComponentModel.DataAnnotations;

namespace DrugStore.Data.Entities.Product
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(500, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters.")]
        public string? ImageName { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }
    }
}
