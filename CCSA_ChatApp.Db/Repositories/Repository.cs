using NHibernate;
usng System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        public Repository(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory.GetSession();
        }
        
        protected readonly ISession _session;
        
        public void Create(T entity)
        {
            _session.Save(entity);
            Commit();
        }
        
        public IEnumerable<T> GetAll()
        {
            var collection = _session.Query<T>();
            return collection;
        }

        public void Update(T obj)
        {
            _session.Update(obj);
            Commit();
        }
      

        public void Delete(T entity)
        {
            _session.Delete(entity);
            Commit();
        }
        
        
         protected bool Commit()
        {
            using var transction = _session.BeginTransaction();

            try
            {
                if (transction.IsActive)
                {

                    _session.Flush();
                    transction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                transction.Rollback();
                return false;
            }
        }

    }
}
