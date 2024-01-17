using _01_WEBAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using linhkien_donet.Entities;

namespace _01_WEBAPI.Dto
{
    public class ProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double OldPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int StatusProduct { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public List<ImageDto> Images { get; set; }

    }
}
