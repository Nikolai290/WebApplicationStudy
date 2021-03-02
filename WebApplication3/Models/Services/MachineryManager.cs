using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private readonly IDbManager dbManager;
        private readonly ValidationManager valid = new ValidationManager();
        private readonly OrderManager orderManager;
        public MachineryManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            orderManager = new OrderManager(dbManager);
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

            var machine = dto.Id > 0 ? dbManager.GetById<Machinery>(dto.Id) : new Machinery();
            if (!String.IsNullOrEmpty(dto.Name) && machine.Name != dto.Name)
                machine.SetName(dto.Name);
            ChangeType(machine, dto.TypeId,out var conflictWorks);


            var model = GetMachineryViewModel(dto.Id);
            model.dto = dto;
            if(conflictWorks !=null || conflictWorks.Count != 0) {
                var moss = conflictWorks.Select(x => x.Parent).Distinct();
                var orders = moss.Select(x => x.Order).Distinct();
                foreach (var order in orders) {
                    model.Conflict.Add(new ConflictOrders(order));
                }
                foreach(var conflict in model.Conflict) {
                    conflict.FreeMachineries = orderManager.GetMachinesForOrder(conflict.Order);
                }
            }

            return model;
        }

        private void ChangeType(Machinery machine, int typeId, out IList<Work> conflictWorks) {
            conflictWorks = null;
            if (machine.Type.Id != typeId) {
                var newType = dbManager.GetById<MachineryType>(typeId);
                if (CompareAreas(machine, newType)) {
                    machine.SetType(newType);
                } else {
                    IList<Work> works = FindConflicts(machine);
                    if (works.Count == 0)
                        machine.SetType(newType);
                    else
                        conflictWorks = works;
                }
            }
        }

        private IList<Work> FindConflicts(Machinery machine) 
            => dbManager.GetAll<Work>().Where(x => x.Parent.MachineryId == machine.Id).ToList();


        private bool CompareAreas(Machinery machine, MachineryType anyType)
            => anyType.Areas.Any(x => machine.Type.Areas.Any(z => x.Id == z.Id));

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
