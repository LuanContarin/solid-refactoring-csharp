using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura.OnlineAuctions.WebApp.Data.EfCore
{
    public sealed class AuctionDao : IAuctionDao
    {
        private readonly AppDbContext _dbContext;

        public AuctionDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Leilao> ListAuctions()
        {
            return _dbContext.Leiloes
                .Include(x => x.Categoria)
            .ToList();
        }

        public Leilao? GetAuctionById(int id)
        {
            return _dbContext.Leiloes.Find(id);
        }

        public void InsertAuction(Leilao auction)
        {
            _dbContext.Leiloes.Add(auction);
            _dbContext.SaveChanges();
        }

        public void UpdateAuction(Leilao auction)
        {
            _dbContext.Leiloes.Update(auction);
            _dbContext.SaveChanges();
        }

        public void DeleteAuction(Leilao auction)
        {
            _dbContext.Leiloes.Remove(auction);
            _dbContext.SaveChanges();
        }
    }
}
