using _01_WEBAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_WEBAPI.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public String Images { get; set; }
        public bool IsThumbail { get; set; }
    }
}
