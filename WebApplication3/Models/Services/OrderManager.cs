using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Models.Services {
    public class OrderManager {
        private IDbManager dbManager;

        private MachineryManager machineryManager;

        public OrderManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            machineryManager = new MachineryManager(dbManager);

        }

        public bool Create(Order obj) => dbManager.Add(obj);

  

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
            var machUnique = machineryManager.GetAll();
            var busyMachs = GetAllBusyMachinesOnThisDateAndShift(order);
            return machUnique.Where(x => !busyMachs.Where(z => x.Id == z.MachineryId).Any()).ToList();
        }

        public List<Order> GetAllOrderOnThisDateAndShift(Order order)
            => GetAll().Where(x => x.Date == order.Date && x.Shift == order.Shift).ToList();

        public List<MachineryOnShift> GetAllBusyMachinesOnThisDateAndShift(Order order) {
            var machs = new List<MachineryOnShift>();
            GetAllOrderOnThisDateAndShift(order).ForEach(x => x.Machineries.ToList().ForEach(x => machs.Add(x))); 
            return machs;
        }


        public Order GetById(int id) {
            Order result;
            try {
                result = dbManager.GetById<Order>(id);
            } catch {
                result = new Order().SetBase(DateTime.Now.Date, 1).SetArea(dbManager.GetById<OrderArea>(1));
            }
            return result;
        }

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
