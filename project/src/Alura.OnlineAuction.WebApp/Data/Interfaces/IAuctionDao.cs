using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface IAuctionDao
    {
        IList<Leilao> ListAuctions();
        Leilao? GetAuctionById(int id);
        void InsertAuction(Leilao auction);
        void UpdateAuction(Leilao auction);
        void DeleteAuction(Leilao auction);
    }
}
