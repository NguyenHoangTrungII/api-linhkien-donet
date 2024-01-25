using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace linhkien_donet.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<CartDetail> Items { get; set; }

        //[ForeignKey("UserId")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }


    }

}
