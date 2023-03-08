using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alura.OnlineAuctions.WebApp.Models
{
    public sealed class Auction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = $"The title is required")]
        [Display(Name = "Title", Prompt = "Type the title of the auction")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Auction floor start")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date")]
        public DateTime? Start { get; set; }

        [Display(Name = "Auction floor end")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date")]
        public DateTime? End { get; set; }

        public int IdCategory { get; set; }
        public Category? Category { get; set; }
        public AuctionStatus Status { get; set; }

        public string PosterUrl => $"/images/poster-{Id}.jpg";
    }
}