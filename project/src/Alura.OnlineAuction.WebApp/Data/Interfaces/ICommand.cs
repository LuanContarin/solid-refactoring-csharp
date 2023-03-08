namespace Alura.OnlineAuctions.WebApp.Data.Interfaces
{
    public interface ICommand<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
