using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Data.Dtos
{
    public class MultipleProductsCreateDto
    {
        [StringLength(50, ErrorMessage = "A categoria do produto não pode exceder 50 caracteres")]
        public string Category { get; set; }
        public string[] Name { get; set; }
    }
}
