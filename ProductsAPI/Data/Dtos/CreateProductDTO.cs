using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Data.Dtos
{
    public class CreateProductDTO
    { 
        [Required(ErrorMessage = "A categoria do produto é obrigatória")]
        [StringLength(50, ErrorMessage = "A categoria do produto não pode exceder 50 caracteres")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatória")]
        public string? Name { get; set; }
    }
}
