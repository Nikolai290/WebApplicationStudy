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
        public IList<Machinery> GetAll() { 

            var machs = dbManager.GetAllByString<Machinery>("Machinery");
            //for (int i = 0; i < machs.Count;) {
            //    if (machs[i] is MachineryOnShift)
            //        machs.Remove(machs[i]);
            //    else
            //        i++;
            //}
            return machs;
        }
            
            
            
        public Machinery GetById(int id) {
            var machs = GetAll();
            return machs.Where(x => x.Id == id).First();
        }
            
            
            
           


    }
}
