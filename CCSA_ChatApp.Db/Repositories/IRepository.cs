using CCSA_ChatApp.Domain.Models;

namespace CCSA_ChatApp.Db.Repositories
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void Delete(T obj);
        IEnumerable<T> GetAll();
        void Update(T obj);

    }
}
