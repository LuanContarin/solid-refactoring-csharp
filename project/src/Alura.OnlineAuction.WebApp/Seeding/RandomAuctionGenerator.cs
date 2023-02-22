using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Seeding
{
    public sealed class RandomAuctionGenerator
    {
        private readonly string _generatedText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut consequat semper viverra nam libero justo laoreet. Ut placerat orci nulla pellentesque dignissim enim sit amet. Cras semper auctor neque vitae. Eu lobortis elementum nibh tellus molestie nunc non blandit massa. Penatibus et magnis dis parturient montes nascetur ridiculus. Bibendum enim facilisis gravida neque convallis. At risus viverra adipiscing at in tellus integer feugiat scelerisque. Turpis egestas pretium aenean pharetra magna ac. Suspendisse ultrices gravida dictum fusce ut. Mauris vitae ultricies leo integer. Senectus et netus et malesuada fames ac turpis egestas. Libero volutpat sed cras ornare. Tristique senectus et netus et malesuada fames ac.";

        private readonly Category[] CategoriesSeed = new Category[6]
        {
            new() { Description = "Entertainment & Games", Image = "images/games.png" },
            new() { Description = "Old Cars", Image = "images/cars.png" },
            new() { Description = "Artworks", Image = "images/arts.png" },
            new() { Description = "Properties", Image = "images/properties.png" },
            new() { Description = "Electronics", Image = "images/technology.png" },
            new() { Description = "Collector Itens", Image = "images/collector.png" }
        };

        public Auction NewAuction
        {
            get
            {
                var auction = new Auction
                {
                    // leilao.Id = random.Next(); Will be defined by generation loop (seed)
                    Category = RandomCategory(),
                };

                auction.Title = $"{auction.Category.Description} - Lot Nº {Random.Shared.Next(500)}";
                auction.Description = $"{auction.Title}. {_generatedText}";
                auction.Status = RandomStatus();

                if (auction.Status is not AuctionStatus.Draft)
                    auction.Start = RandomDate();

                if (auction.Status is AuctionStatus.Finished)
                {
                    var previousDate = DateTime.Now.AddDays(-Random.Shared.Next(10));
                    auction.End = auction.Start.Value.CompareTo(previousDate) > 0
                        ? previousDate
                        : auction.Start.Value;
                }

                auction.IdCategory = auction.Category.Id;
                return auction;
            }
        }

        private Category RandomCategory()
        {
            var index = Random.Shared.Next(0, 5);
            return CategoriesSeed[index];
        }

        private DateTime RandomDate()
        {
            int date = Random.Shared.Next(1, 100);
            return DateTime.Now.AddDays(-date);
        }

        private AuctionStatus RandomStatus()
        {
            int index = Random.Shared.Next(0, 3);
            var values = Enum.GetValues(typeof(AuctionStatus));
            return (AuctionStatus)values.GetValue(index);
        }
    }
}