using System.ComponentModel.DataAnnotations;

namespace OrdersAPI
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
              
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "O nome da loja é obrigatório")]
        public string StoreName { get; set; }
    }
}