using Alura.OnlineAuctions.WebApp.Data;
using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Seeding
{
    public static class DatabaseGenerator
    {
        public static void Seed()
        {
            using var ctx = new AppDbContext();
            if (ctx.Database.EnsureCreated())
            {
                var generator = new RandomAuctionGenerator();
                var auctions = new List<Auction>();

                for (var i = 1; i <= 200; i++)
                {
                    auctions.Add(generator.NewAuction);
                }

                ctx.Auctions.AddRange(auctions);
                ctx.SaveChanges();
            }
        }
    }
}