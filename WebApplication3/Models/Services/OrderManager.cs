using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Models.Services {
    public class OrderManager {
        private IDbManager dbManager;

        private EmployeeManager employeeManager;
        private MachineryManager machineryManager;

        public OrderManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            employeeManager = new EmployeeManager(dbManager);
            machineryManager = new MachineryManager(dbManager);

        }

        public bool SaveOrUpdate(Order obj) {

            var result = false;

            if (AlreadyExist(obj))
                result = Update(obj);
            else
                result = dbManager.Add<Order>(obj);

            return result;
        }

        private bool AlreadyExist(Order obj) {
            var result = false;
            result = CheckId(obj);
            result = CheckRepeat(obj);


            return result;
        }

        private bool CheckRepeat(Order obj)
            => (GetAll().Where(x =>
            x.Date == obj.Date &&
            x.Shift == obj.Shift &&
            x.Area.Id == obj.Area.Id)).Any();

        private Order Find(Order obj)
            => (GetAll().Where(x =>
            x.Date == obj.Date &&
            x.Shift == obj.Shift &&
            x.Area.Id == obj.Area.Id)).First();

        public IList<Machinery> GetAddingListMachinesExcludeRepeats(Order order) {
            var result = dbManager.GetAll<Machinery>().ToList();
            foreach (var m in order?.Machineries?.Select(x => x.Name)) {
                result.Remove(result.Where(z => z.Name == m).First());
            }
            return result;
        }

        public Order GetById(int id)
            => dbManager.GetById<Order>(id);

        private bool CheckId(Order obj)
            => GetAll().Where(x => x.Id == obj.Id).Any();

        // Сделать метод апдейт
        private bool Update(Order obj) {

            Order order = Find(obj);
            order = obj.CopyTo(order);
            return dbManager.Update(order);
        }

        public bool Delete(Order obj) => dbManager.Delete<Order>(obj);
        public IList<Order> GetAll() => dbManager.GetAll<Order>();
        public Order Get(DateTime date, int shift, int orderAreaId) {
            // date format: ("yyyy-MM-dd")
            Order result;
            try {
                result = GetAll().Where(x => x.Date == date && x.Shift == shift && x.Area.Id == orderAreaId).First();


            } catch {
                result = new Order().SetBase(date, shift).SetArea(dbManager.GetById<OrderArea>(orderAreaId));
            }


            return result;
        }


        public Order SetArea(Order order, int areaId) {
            order.SetArea(dbManager.GetById<OrderArea>(areaId));
            return order;
        }

    }
}
