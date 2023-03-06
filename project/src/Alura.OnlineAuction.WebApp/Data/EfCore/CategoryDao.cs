using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura.OnlineAuctions.WebApp.Data.EfCore
{
    public sealed class CategoryDao : ICategoryDao
    {
        private readonly AppDbContext _dbContext;

        public CategoryDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Category> ListCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public IList<Category> ListCategoriesWithAuctions()
        {
            return _dbContext.Categories
                .Include(c => c.Auctions)
                .ToList();
        }

        public Category? GetCategoryWithAuctionById(int id)
        {
            return _dbContext.Categories
                .Include(c => c.Auctions)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
