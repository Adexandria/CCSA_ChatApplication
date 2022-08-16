using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
