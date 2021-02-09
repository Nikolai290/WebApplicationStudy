using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Models.Services {
    public class OrderManager {

        DbManager dbManager = new DbManager();
        EmployeeManager employeeManager = new EmployeeManager();
        MachineryManager machineryManager = new MachineryManager();

        public Order Order { get; set; }

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
            // date format: ("dd.MM.yyyy")
            Order result;
            try {
                result = GetAll().Where(x => x.Date == date && x.Shift == shift && x.Area.Id == orderAreaId).First();
                //Employee disp;
                //Employee chief;
                //IList<Employee> masters;
                //IList<MachineryOnShift> machs;
                //result = dbManager.GetOrderById(result.Id);
                //result.SetStaff(disp, chief, masters);
                //Compare(result);

            } catch {
                result = new Order().SetBase(date, shift).SetArea(dbManager.GetById<OrderArea>(orderAreaId));
            }


            return result;
        }

        private Order Compare(Order order) {


            var disp = employeeManager.GetEmployeeById(order.Dispetcher.Id);
            var chief = employeeManager.GetEmployeeById(order.Chief.Id);
            IList<Employee> masters = new List<Employee>();
            foreach (var master in order.MiningMaster) {
                masters.Add(employeeManager.GetEmployeeById(master.Id));
            }

            order = order.SetStaff(disp, chief, masters);
            //IList<MachineryOnShift> machs = new List<MachineryOnShift>();
            //foreach (var m in order.Machineries) {
            //}

            return order;
        }

        public Order SetArea(Order order, int areaId) {
            order.SetArea(dbManager.GetById<OrderArea>(areaId));
            return order;
        }

    }
}
