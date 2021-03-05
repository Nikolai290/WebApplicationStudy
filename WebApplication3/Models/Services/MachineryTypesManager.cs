using System;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.ViewModels.Machineries;

namespace WebApplication3.Models.Services {
    public class MachineryTypesManager {
        private readonly IDbManager dbManager;

        public MachineryTypesManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public MachinariesTypeViewModel FillViewModel(MachinariesTypeViewModel model = null) {
            if (model == null) model = new MachinariesTypeViewModel();
            model.Areas = dbManager.GetAll<OrderArea>().ToList();
            model.Types = dbManager.GetAll<MachineryType>().ToList();
            model.Title = model.Id > 0 ? "Редактирование" : "Добавление";

            return model;
        }

        internal MachinariesTypeViewModel GetMachineryTypesViewModel(int id = 0, MachinariesTypeViewModel model = null) {
            if (model == null) {
                model = new MachinariesTypeViewModel();
                model.Id = id;
            }
            
            model = FillViewModel(model);
            var type = model.Id > 0 ?
                 dbManager.GetById<MachineryType>(model.Id) :
                 new MachineryType();
            model.CopyFrom(type);
            return model;
        }

        internal MachinariesTypeViewModel SaveOrUpdateMachineryType(MachinariesTypeViewModel dto) {
            // validation

                var type = dto.Id > 0 ? dbManager.GetById<MachineryType>(dto.Id): new MachineryType();
                type.Name = dto.Name;
                type.Areas = dbManager.GetByListId<OrderArea>(dto.AreasId).ToList();
                if (dto.Id == 0) dbManager.AddAsync<MachineryType>(type);
            
            var model = GetMachineryTypesViewModel(dto.Id);
            model.Message = "";

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
    }
}
