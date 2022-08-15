using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Db
{
    public class SessionFactory
    {
        public SessionFactory(IConfiguration configuration)
        {
            if (_sessionFactory is null)
            {
                string _connectionString = configuration.GetConnectionString("ChatApp");
                _sessionFactory = BuildSessionFactory(_connectionString);
            }
        }

        public ISession GetSession() => _sessionFactory.OpenSession();

        private readonly ISessionFactory _sessionFactory;

        private ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof().Assembly))
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(true, true);
                });
            return configuration.BuildSessionFactory();
        }

       
    }
}

