using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Data.Dtos
{
    public class UpdateOrderDTO
    {
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string? ClientName { get; set; }

        [Required(ErrorMessage = "O nome da loja é obrigatório")]
        public string? StoreName { get; set; }
    }
}
