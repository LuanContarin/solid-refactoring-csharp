using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface IAuctionDao
    {
        IList<Auction> ListAuctions();
        Auction? GetAuctionById(int id);
        void InsertAuction(Auction auction);
        void UpdateAuction(Auction auction);
        void DeleteAuction(Auction auction);
    }
}
