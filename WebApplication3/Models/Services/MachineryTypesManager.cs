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

        internal MachinariesTypeViewModel GetMachineryTypesViewModel(int id) {
            var model = new MachinariesTypeViewModel();
            model.Areas = dbManager.GetAll<OrderArea>().ToList();
            model.Types = dbManager.GetAll<MachineryType>().ToList();
            model.Type = id > 0 ?
                 dbManager.GetById<MachineryType>(id) :
                 new MachineryType();
            model.Title = id > 0 ? "Редактирование" : "Добавление";
            return model;
        }

        internal MachinariesTypeViewModel SaveOrUpdateMachineryType(MachineryTypeDTO dto) {
            var check = Check(dto, out string message);
            if (check) {
                var type = dto.Id > 0 ? dbManager.GetById<MachineryType>(dto.Id): new MachineryType();
                type.Name = check ? dto.Name : type.Name;
                type.Areas = dbManager.GetByListId<OrderArea>(dto.AreasId).ToList();
                if (dto.Id == 0) dbManager.AddAsync<MachineryType>(type);
            }
            var model = GetMachineryTypesViewModel(dto.Id);
            model.Message = message;

            return model;
        }

        internal MachinariesTypeViewModel Delete(int typeId) {
            var isDelete = dbManager.PseudoDelete<MachineryType>(typeId);
            var model = GetMachineryTypesViewModel(0);
            model.Message = isDelete ?
                "Объект удалён" :
                "Не удалось";

            return model;
        }

        private bool Check(MachineryTypeDTO dto, out string message) {
            if (String.IsNullOrEmpty(dto.Name)) {
                message = "Строка не должна быть пустой";
                return false;
            }
            if (dto.Name.Length < 4) {
                message = "Строка должна содержать 5 и более символов";
            return false;
            }
            if(dto.AreasId.Count == 0) {
                message = "Необходимо выбрать участок, соответствующий типу";
                return false;
            }
            message = "Успешно";
            return true;
        }

    }
}
