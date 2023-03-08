using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface ICategoryDao : IQuery<Category>
    {
        public IList<Category> GetAllWithAuctions();
        public Category? GetByIdWithAuctions(int id);
    }
}
