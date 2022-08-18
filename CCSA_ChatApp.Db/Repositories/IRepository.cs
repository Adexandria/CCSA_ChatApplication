using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task Delete(T entity);
        IEnumerable<T> GetAll();
        Task Update(T obj);  
    }
}
