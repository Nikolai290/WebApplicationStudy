using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private IDbManager dbManager;

        public MachineryManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool Create(Machinery obj) => dbManager.Add<Machinery>(obj);
        public bool Update(Machinery obj) => dbManager.Update<Machinery>(obj);
        public bool Delete(Machinery obj) => dbManager.Delete<Machinery>(obj);
        public IList<Machinery> GetAll() => dbManager.GetAll<Machinery>();
        public Machinery GetAll(int id) => dbManager.GetById<Machinery>(id);


    }
}
