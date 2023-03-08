namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface IQuery<T>
    {
        public IList<T> GetAll();
        public T? GetbyId(int id);
    }
}
