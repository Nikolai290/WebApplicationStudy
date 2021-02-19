using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using WebApplication3.Models.Entities;


namespace WebApplication3.Models.DbAccess {
    public class DbManager : IDbManager {
        private readonly ISession session;
        private readonly ITransaction transaction;

        public DbManager() {
            session = DbAccess.GetInstance().GetSessionFactory().OpenSession();
            transaction = session.BeginTransaction();

        }

        public void Commit() {
            transaction.Commit();
            session.Close();
        }

        public bool AddAsync<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.SaveOrUpdateAsync(obj);
            } catch {
                result = false;
            }
            return result;
        }




        public bool DeleteById<T>(int id) where T : DbEntities {
            bool result = true;
            T obj = GetById<T>(id);

            if (obj != null)
                DeleteAsync(obj);
            else
                result = false;

            return result;
        }

        public bool DeleteAsync<T>(T obj) where T : DbEntities {
            bool result = true;
            try {
                session.DeleteAsync(obj);
            } catch {
                result = false;
            }
            return result;
        }



        public IQueryable<T> GetAll<T>() where T : DbEntities
        => session.Query<T>();

        public async Task<T> GetByIdAsync<T>(int id) where T : DbEntities
            => await session.GetAsync<T>(id);

        public T GetById<T>(int id) where T : DbEntities
            => GetAll<T>().Single(x => x.Id == id);




            public bool UpdateAsync<T>(T obj) where T : DbEntities {
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

        public async Task<IList<T>> GetByListAsync<T>(IList<int> ids) where T : DbEntities {
            var result = new List<T>();
            foreach (var id in ids) {
                result.Add( await session.GetAsync<T>(id));
            }
            return result;
        }
    }
}
