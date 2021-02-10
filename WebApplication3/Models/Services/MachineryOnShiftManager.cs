using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class MachineryOnShiftManager {
        private IDbManager dbManager;

        public MachineryOnShiftManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool Create(MachineryOnShift obj) => dbManager.Add<MachineryOnShift>(obj);
        public bool Update(MachineryOnShift obj) => dbManager.Update<MachineryOnShift>(obj);
        public bool Delete(MachineryOnShift obj) => dbManager.Delete<MachineryOnShift>(obj);
        public IList<MachineryOnShift> GetAll() => dbManager.GetAll<MachineryOnShift>();
        public MachineryOnShift GetById(int id) => dbManager.GetById<MachineryOnShift>(id);
        public IList<MachineryOnShift> GetByOrder(int orderId)
            => GetAll().Where(x => x.Order.Id == orderId).ToList();


        public IList<QuarryArea> GetAreas()
            => dbManager.GetAll<QuarryArea>();
        public IList<QuarryField> GetFields()
             => dbManager.GetAll<QuarryField>();

        public IList<QuarryHorizon> GetHorizons()
             => dbManager.GetAll<QuarryHorizon>();

        public IList<QuarryPlast> GetPlasts()
             => dbManager.GetAll<QuarryPlast>();

        public IList<Group> GetGroups()
            => dbManager.GetAll<Group>();
    }
}
