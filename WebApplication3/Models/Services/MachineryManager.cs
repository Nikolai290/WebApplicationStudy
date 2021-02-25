using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private readonly IDbManager dbManager;
        private readonly ValidationManager valid = new ValidationManager();

        public MachineryManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public MachineriesViewModel GetMachineryViewModel(int id) {
            var model = new MachineriesViewModel();
            model.Machine = id > 0 ? 
                dbManager.GetById<Machinery>(id) : 
                new Machinery();
            model.Machineries = dbManager.GetAll<Machinery>().ToList();
            model.Types = dbManager.GetAll<MachineryType>().ToList();
            model.Title = id > 0 ? "Редактирование" : "Добавление";
            return model;
        }

        public MachineriesViewModel SaveOrUpdateMachinery(MachineryDTO dto) {
            var check = valid.CheckMachineryDTO(dto, out string message);
            if (check) {
                var machine = dto.Id > 0 ? dbManager.GetById<Machinery>(dto.Id) : new Machinery();
                machine.SetName(dto.Name).SetType(dbManager.GetById<MachineryType>(dto.TypeId));
                if (dto.Id == 0) dbManager.AddAsync(machine);
            }
            var model = GetMachineryViewModel(dto.Id);
            model.Message = message;


            return model;
        }

        internal MachineriesViewModel Delete(int machineId) {
            var isDelete = dbManager.PseudoDelete<Machinery>(machineId);
            var model = GetMachineryViewModel(0);
            model.Message = isDelete ?
                "Объект удалён" :
                "Не удалось";

            return model;
        }
    }
}
