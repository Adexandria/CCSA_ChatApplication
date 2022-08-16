using CCSA_ChatApp.Domain.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db.Repositories
{
    public class MessageRepository 
    {
        public MessageRepository(SessionFactory sessionFactory)
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
        
        public void CreateMessage(Message message)
        {
            _session.Save(message);
            Commit();
            CreateMessageHistory(message);
        }

        public void CreateMessageHistory(Message message,)
        {

        }

        public void Delete(Message entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
