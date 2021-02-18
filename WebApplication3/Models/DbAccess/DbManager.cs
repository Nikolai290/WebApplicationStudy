using System.Collections.Generic;
using System.Linq;
using NHibernate;
using WebApplication3.Models.Entities;


namespace WebApplication3.Models.DbAccess {
    public class DbManager : IDbManager {
        public static bool IsEmpty { get; private set; }
        private ISession session;
        private ITransaction transaction;

        public DbManager() {
            session = DbAccess.GetInstance().GetSessionFactory().OpenSession();
            transaction = session.BeginTransaction();

        }

        public void Commit() {
            transaction.Commit();
            session.Close();
        }

        public bool Add<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.SaveOrUpdate(obj);
            } catch {
                result = false;
            }
            return result;
        }

        public bool DeleteById<T>(int id) where T : DbEntities {
            bool result = true;
            T obj = GetById<T>(id);

            if (obj != null)
                Delete(obj);
            else
                result = false;

            return result;
        }

        public bool Delete<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.Delete(obj);
            } catch {
                result = false;
            }
            return result;
        }

        public IQueryable<T> GetAll<T>() where T : DbEntities 
            => session.Query<T>();




        public T GetById<T>(int id) where T : DbEntities 
            => GetAll<T>().Single(x => x.Id == id);


        public bool Update<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.Update(obj);
            } catch {
                result = false;
            }
            return result;
        }

        public IList<T> GetByListId<T>(IList<int> ids) where T : DbEntities {
            return GetAll<T>().Where(x => ids.Contains(x.Id)).ToList();
        }
    }
}
