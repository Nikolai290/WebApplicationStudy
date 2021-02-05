using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;

namespace WebApplication3.Models.Services {
    public class OrderManager {

        DbManager dbManager = new DbManager();


        public bool Create(Order obj) => dbManager.Add<Order>(obj);
        public bool Update(Order obj) => dbManager.Update<Order>(obj);
        public bool Delete(Order obj) => dbManager.Delete<Order>(obj);
        public IList<Order> GetAll() => dbManager.GetAll<Order>();
        public Order Get(DateTime date, int shift, int orderAreaId) 
            => GetAll().Where(x => x.Date == date && x.Shift == shift && x.Area.Id == orderAreaId).First();


    }
}
