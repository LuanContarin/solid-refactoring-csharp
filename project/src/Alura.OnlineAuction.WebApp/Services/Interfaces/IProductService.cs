using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Services.Interfaces
{
    public interface IProductService
    {
        IList<Auction> ListAuctionsBySearch(string search);
        IList<CategoryWithInfoAuction> ListCategoriesWithAuctions();
        Category? GetCategoryWithAuctionsById(int id);
    }
}
