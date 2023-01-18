using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Data.Dtos
{
    public class ReadProductDTO
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
    }
}
