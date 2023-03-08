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

        public IList<Category> GetAllWithAuctions()
        {
            return _dbContext.Categories
                .Include(c => c.Auctions)
                .ToList();
        }

        public Category? GetByIdWithAuctions(int id)
        {
            return _dbContext.Categories
                .Include(c => c.Auctions)
                .FirstOrDefault(c => c.Id == id);
        }

        public IList<Category> GetAll()
        {
            return _dbContext.Categories
                .ToList();
        }

        public Category? GetbyId(int id)
        {
            return _dbContext.Categories
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
