using System.ComponentModel.DataAnnotations;

namespace ProductsAPI
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "A categoria do produto é obrigatória")]
        [MaxLength(50, ErrorMessage = "A categoria do produto não pode exceder 50 caracteres")]
        public string Category { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string Name { get; set; }
        
    }
}