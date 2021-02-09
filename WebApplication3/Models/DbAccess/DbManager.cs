using System.Collections.Generic;
using System.Linq;
using NHibernate;
using WebApplication3.Models.Entities;


namespace WebApplication3.Models.DbAccess {
    public class DbManager {

        ISessionFactory sessionFactory = DbAccess.GetInstance().GetSessionFactory();
        public static bool IsEmpty { get; private set; }

        public bool Add<T>(T obj) where T : DbEntities {
            bool result = true;
            using var session = sessionFactory.OpenSession();
            using var tx = session.BeginTransaction();
            try {
                session.SaveOrUpdate(obj);
                tx.Commit();
            } catch {
                result = false;
            }
            return result;
        }

        public bool DeleteById<T>(int id) where T : DbEntities {
            bool result = true;
            T obj = GetById<T>(id);

            using var session = sessionFactory.OpenSession();
            using var tx = session.BeginTransaction();


            if (obj != null)
                Delete(obj);
            else
                result = false;

            tx.Commit();
            return result;
        }

        public bool Delete<T>(T obj) where T : DbEntities {

            bool result = true;
            using var session = sessionFactory.OpenSession();
            using var tx = session.BeginTransaction();
            try {
                session.Delete(obj);
            } catch {
                result = false;
            }
            tx.Commit();
            return result;

        }

        public IList<T> GetAll<T>() where T : DbEntities {
            IList<T> result;

            using var session = sessionFactory.OpenSession();
            result = session.QueryOver<T>().List();
            if (result.Count == 0)
                IsEmpty = true;

            return result;
        }


        public T GetById<T>(int id) where T : DbEntities {
            T result = null;

            using var session = sessionFactory.OpenSession();
            result = session.QueryOver<T>()?.List()?.Where(x => x.Id == id)?.First();

            return result;
        }

        public bool Update<T>(T obj) where T : DbEntities {
            bool result = true;
            using var session = sessionFactory.OpenSession();
            using var tx = session.BeginTransaction();
            try {
                session.Save(obj);
            } catch {
                result = false;
            }
            tx.Commit();
            return result;
        }

        public T Get<T>(T obj) where T : DbEntities {
            using var session = sessionFactory.OpenSession();
            return session.Get<T>(obj);
        }

        public Order GetOrderById(int id) {
            //Employee disp;
            //Employee chief;
            //IList<Employee> masters;
            //IList<MachineryOnShift> machs;
            using var session = sessionFactory.OpenSession();
            //using var tx = session.BeginTransaction();
            Order result = session.Get<Order>(id);
            //disp = result.Dispetcher;
            //chief = result.Chief;
            //masters = result.MiningMaster;
            //machs = result.Machineries;
         
            //tx.Commit();
            //session.Load("Order", id);
            //session.Load<Order>(id);
            return result;
        }
    }
}
