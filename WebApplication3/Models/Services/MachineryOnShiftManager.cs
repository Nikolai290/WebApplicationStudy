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
        public bool Delete(MachineryOnShift obj) {
            obj.SetNulls();
            return dbManager.Delete(obj);
        }
        public bool DeleteById(int id) => Delete(GetById(id));

        public IList<MachineryOnShift> GetAll() => dbManager.GetAll<MachineryOnShift>();
        public MachineryOnShift GetById(int id) => dbManager.GetById<MachineryOnShift>(id);
        public Machinery GetMachineryById(int id) => dbManager.GetById<Machinery>(id);
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
