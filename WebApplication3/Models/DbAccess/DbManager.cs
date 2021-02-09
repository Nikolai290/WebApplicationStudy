using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using WebApplication3.Models.Entities;


namespace WebApplication3.Models.DbAccess {
    public class DbManager : IDbManager {
        public static bool IsEmpty { get; private set; }

        //private ISessionFactory sessionFactory = DbAccess.GetInstance().GetSessionFactory();
        private ISession session;
        private ITransaction transaction;

        public DbManager() {
            //using var session = sessionFactory.OpenSession();
            //using var transaction = session.BeginTransaction();
            //this.session = session;
            //this.transaction = transaction;
            session = DbAccess.GetInstance().GetSessionFactory().OpenSession();
            transaction = session.BeginTransaction();

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

        public void Commit() {
            transaction.Commit();
            session.Close();
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
                session.Save(obj);
            } catch {
                result = false;
            }
            return result;
        }

        public T Get<T>(T obj) where T : DbEntities {
            return session.Get<T>(obj);
        }

        public Order GetOrderById(int id) {
            //Employee disp;
            //Employee chief;
            //IList<Employee> masters;
            //IList<MachineryOnShift> machs;
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
