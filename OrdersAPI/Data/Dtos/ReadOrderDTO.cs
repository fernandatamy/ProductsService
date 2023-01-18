using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Data.Dtos
{
    public class ReadOrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string StoreName { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
