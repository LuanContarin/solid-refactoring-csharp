using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface IAuctionDao : IQuery<Auction>, ICommand<Auction>
    {
        IList<Auction> SearchAuction(string search);
    }
}
