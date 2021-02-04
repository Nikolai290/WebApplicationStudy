using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace WebApplication3.Models.DbAccess {

    public class DbAccess {

        private static string ConnectionString;
        private ISessionFactory sessionFactory;

        private static DbAccess instance;
        private static readonly object block = new object();



        private DbAccess() {
        }

        public static DbAccess GetInstance() {

            if(instance == null) {
                lock (block) {
                    if (instance == null) {
                        instance = new DbAccess();
                    }
                }
            }

            return instance;
        }



        public ISessionFactory GetSessionFactory() {
            return sessionFactory;
        }

        public void SetConnectionString(string connectionString) {
           ConnectionString = connectionString;
           sessionFactory = CreateSessionFactory();

        }

        private ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(ConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config) {
            if (File.Exists(ConnectionString)) {
               // File.Delete(connectionString);
            }

            new SchemaExport(config)
                .Create(false, true);
        }
    }
}
