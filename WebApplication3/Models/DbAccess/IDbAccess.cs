using NHibernate;

namespace WebApplication3.Models.DbAccess {
    interface IDbAccess {
        public abstract ISessionFactory GetSessionFactory();
    }
}
