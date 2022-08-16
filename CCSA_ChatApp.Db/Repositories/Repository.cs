﻿using NHibernate;
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
            _session = sessionFactory.GetSession();
        }
        protected readonly ISession _session;

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
        public void Create(T entity)
        {
            _session.Save(entity);
            Commit();
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
            Commit();
        }
    }
}