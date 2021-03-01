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
                session.SaveAsync(obj);
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
        => session.Query<T>().Where(x => !x.IsDelete);

        public IQueryable<T> GetAllForce<T>() where T : DbEntities
            => session.Query<T>();
        public async Task<T> GetByIdAsync<T>(int id) where T : DbEntities {
            return await session.GetAsync<T>(id);
        }

        public T GetById<T>(int id) where T : DbEntities {
            try {
                var obj = GetAll<T>().Single(x => x.Id == id);
                return obj;
            } catch {
                return null;
            }

        }
        public T GetByIdForce<T>(int id) where T : DbEntities {
            try {
                var obj = GetAllForce<T>().Single(x => x.Id == id);
                return obj;
            } catch {
                return null;
            }
        }

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
            return ids.Count > 0 ? GetAll<T>().Where(x => ids.Contains(x.Id)).ToList() : new List<T>();
        }

        public async Task<IList<T>> GetByListAsync<T>(IList<int> ids) where T : DbEntities {
            var result = new List<T>();
            foreach (var id in ids) {
                result.Add(await session.GetAsync<T>(id));
            }
            return result;
        }


        public bool PseudoDelete<T>(int id) where T : DbEntities {
            try {
                var obj = GetById<T>(id);
                obj.Delete(true);
                return true;
            } catch {
                return false;
            }

        }

        public bool PseudoDelete<T>(T obj) where T : DbEntities {
            try {
                obj.Delete(true);
                return true;
            } catch {
                return false;
            }
        }
    }
}

