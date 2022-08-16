using CCSA_ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using NHibernate;
using System;
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
            _sessionFactory = sessionFactory;
        }

        

        public void Create(T entity)
        {
            _sessionFactory.GetSession().Save(entity);
            Commit();
        }


        public IEnumerable<T> GetAll()
        {
            var collection = _sessionFactory.GetSession().Query<T>();
            return collection;
        }

        public void Update(T obj)
        {
            _sessionFactory.GetSession().Update(obj);
            Commit();
        }


        public void Delete(T obj)
        {
            _sessionFactory.GetSession().Delete(obj);
            Commit();
        }

        protected bool Commit()
        {
            using var transction = _sessionFactory.GetSession().BeginTransaction();
            try
            {
                if (transction.IsActive)
                {
                    _sessionFactory.GetSession().Flush();
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


        private ITransaction OpenTransaction()
        {
            return _sessionFactory.GetSession().BeginTransaction();
        }

        

        protected readonly SessionFactory _sessionFactory;


    }
}
