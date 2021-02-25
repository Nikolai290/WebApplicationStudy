using System;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class MachineryTypesManager {
        private readonly IDbManager dbManager;

        public MachineryTypesManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        internal MachinariesTypeViewModel GetMachineryTypesViewModel(int typeId) {
            var model = new MachinariesTypeViewModel();
            model.Areas = dbManager.GetAll<OrderArea>().ToList();
            model.Types = dbManager.GetAll<MachineryType>().ToList();
            model.Type = typeId == 0 ?
                new MachineryType() :
                dbManager.GetById<MachineryType>(typeId);

            return model;
        }

        internal MachinariesTypeViewModel SaveOrUpdateMachineryType(MachineryTypeDTO dto) {
            var model = GetMachineryTypesViewModel(dto.Id);
            var check = Check(dto.Name, out string message);
            model.Message = message;
            if (check) {
                model.Type.Name = check ? dto.Name : model.Type.Name;
                model.Type.Areas = dbManager.GetByListId<OrderArea>(dto.AreasId).ToList();
                if (dto.Id == 0) dbManager.AddAsync<MachineryType>(model.Type);
            }

            return model;
        }

        internal MachinariesTypeViewModel Delete(int typeId) {
            var model = GetMachineryTypesViewModel(0);
            var isDelete = dbManager.PseudoDelete<MachineryType>(typeId);
            model.Message = isDelete ?
                "Объект удалён" :
                "Не удалось";

            return model;
        }

        private bool Check(string line, out string message) {
            if (String.IsNullOrEmpty(line)) {
                message = "Строка не должна быть пустой";
                return false;
            }

            if (line.Length < 4) {
                message = "Строка должна содержать 5 и более символов";
            return false;
            }
            message = "Успешно";
            return true;
        }

    }
}
