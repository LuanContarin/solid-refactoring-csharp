using System.ComponentModel.DataAnnotations.Schema;

namespace Alura.OnlineAuctions.WebApp.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<Auction> Auctions { get; set; }
    }

    public sealed class CategoryWithInfoAuction : Category
    {
        public int InDraft { get; set; }
        public int InFloor { get; set; }
        public int Finalized { get; set; }
    }
}