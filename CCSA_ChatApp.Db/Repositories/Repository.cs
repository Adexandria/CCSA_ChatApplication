using NHibernate;

namespace CCSA_ChatApp.Db.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        public Repository(SessionFactory sessionFactory)
        {
            _session = sessionFactory.GetSession();
        }
        
        protected readonly ISession _session;
        
        public async Task Create(T entity)
        {
            await _session.SaveAsync(entity);
            await Commit();
        }
        
        public IEnumerable<T> GetAll()
        {
            var collection = _session.Query<T>();
            return collection;
        }

        public async Task Update(T obj)
        {
            await _session.UpdateAsync(obj);
            await Commit();
        }
      

        public async Task Delete(T entity)
        {
            await _session.DeleteAsync(entity);
            await Commit();
        }
        
        
         protected async Task<bool> Commit()
        {
            using var transction = _session.BeginTransaction();

            try
            {
                if (transction.IsActive)
                {
                    _session.Flush();
                    await transction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
