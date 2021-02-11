using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace WebApplication3.Models.DbAccess {

    public class DbAccess {

        private static string connectionString;
        private ISessionFactory sessionFactory;
        private static bool rebuild = true;

        private static DbAccess instance;
        private static readonly object block = new object();
        private DbAccess() { }

        public static DbAccess GetInstance() {

            if (instance == null) {
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
            DbAccess.connectionString = connectionString;
            sessionFactory = CreateSessionFactory();

        }

        private ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config) {
            //if (!File.Exists(connectionString)) {
            //    new SchemaExport(config)
            //        .Create(false, true);
            //} else 
            if (rebuild && File.Exists(connectionString)) {
                sessionFactory?.Close();
                File.Delete(connectionString);
            }
            if (!File.Exists(connectionString)) {
                new SchemaExport(config)
                    .Create(false, true);
            }
        }

        public static void Rebuild() {
            rebuild = true;
            instance.CreateSessionFactory();
            rebuild = false;

        }
    }
}
