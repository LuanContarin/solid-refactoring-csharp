using Alura.OnlineAuctions.WebApp.Data.Interfaces;
using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.EfCore
{
    public sealed class CategoryDao : ICategoryDao
    {
        private readonly AppDbContext _dbContext;

        public CategoryDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Categoria> ListCategories()
        {
            return _dbContext.Categorias.ToList();
        }
    }
}
