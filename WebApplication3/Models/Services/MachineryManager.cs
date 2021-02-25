using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private readonly IDbManager dbManager;

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
            var check = Check(dto, out string message);
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

        private bool Check(MachineryDTO dto, out string message) {
            if (String.IsNullOrEmpty(dto.Name)) {
                message = "Строка не должна быть пустой";
                return false;
            }
            if (dto.Name.Length < 4) {
                message = "Строка должна содержать 5 и более символов";
                return false;
            }
            if (dto.TypeId == 0) {
                message = "Необходимо выбрать тип";
                return false;
            }
            message = "Успешно";
            return true;
        }
    }
}
