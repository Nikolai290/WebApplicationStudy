using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private readonly IDbManager dbManager;

        public MachineryManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public MachineriesViewModel GetMachineryViewModel(int id) {
            var model = new MachineriesViewModel();
            model.Machine = id == 0 ? new Machinery() : dbManager.GetById<Machinery>(id);
            model.Machineries = dbManager.GetAll<Machinery>().ToList();
            model.Types = dbManager.GetAll<MachineryType>().ToList();

            return model;
        }

        public MachineriesViewModel SaveOrUpdateMachinery(MachineryDTO dto) {

            var model = GetMachineryViewModel(dto.Id);
            model.Machine.SetName(dto.Name).SetType(dbManager.GetById<MachineryType>(dto.TypeId));

            return model;
        }

        internal void Delete(int machineId) {
            dbManager.PseudoDelete<Machinery>(machineId);
        }
    }
}
