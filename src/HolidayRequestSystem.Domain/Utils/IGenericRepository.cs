namespace HolidayRequestSystem.Domain.Utils
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        void Add(T entity);
    }
}
