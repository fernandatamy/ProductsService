using System.ComponentModel.DataAnnotations;

namespace OrdersAPI
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
              
        [Required(ErrorMessage = "O nome do cliente � obrigat�rio")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "O nome da loja � obrigat�rio")]
        public string StoreName { get; set; }
    }
}