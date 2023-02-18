using Alura.OnlineAuctions.WebApp.Models;

namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface ICategoryDao
    {
        IList<Categoria> ListCategories();
    }
}
