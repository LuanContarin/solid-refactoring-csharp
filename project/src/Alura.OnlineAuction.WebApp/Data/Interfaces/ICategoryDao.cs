using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface ICategoryDao
    {
        IList<Category> ListCategories();
        IList<Category> ListCategoriesWithAuctions();
    }
}
