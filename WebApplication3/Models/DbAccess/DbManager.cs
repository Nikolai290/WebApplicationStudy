using System;
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
        public IList<T> GetAllByString<T>(string entityName) where T : DbEntities {
            IList<T> result;
            result = session.QueryOver<T>(entityName).List();

            return result;
        }

        public IList<T> GetAll<T>() where T : DbEntities {
            IList<T> result;

            result = session.QueryOver<T>().List();
            if (result.Count == 0)
                IsEmpty = true;
            return result;
        }


        public T GetById<T>(int id) where T : DbEntities {
            T result = null;

            result = session.QueryOver<T>()?.List()?.Where(x => x.Id == id)?.First();

            return result;
        }

        public bool Update<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.Update(obj);
            } catch {
                result = false;
            }
            return result;
        }
    }
}
