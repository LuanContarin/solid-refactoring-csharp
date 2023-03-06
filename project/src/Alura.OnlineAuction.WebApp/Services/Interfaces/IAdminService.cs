using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Services.Interfaces
{
    public interface IAdminService
    {
        public IList<Category> ListCategories();
        public IList<Auction> ListAuctions();
        Auction? GetAuctionById(int id);
        IList<Auction> ListAuctionsBySearch(string search);
        void InsertAuction(Auction auction);
        void UpdateAuction(Auction auction);
        void DeleteAuction(Auction auction);
        void StartAuctionFloorById(int id);
        void FinishAuctionFloorById(int id);
    }
}
