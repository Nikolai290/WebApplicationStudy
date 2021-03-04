using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace WebApplication3.Models.Services {
    public class MachineryManager {
        private readonly IDbManager dbManager;
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

        // to do: decomposition
        public bool SolveConflict(IList<SolveConflictDTO> dto) {
            if (dto.Any(x => x.MachineId == 0))
                return false;

            // Группируем по Id машин в наряде, которые будем заменять
            var groupByMosid = dto.GroupBy(x => x.MoSId);
            foreach (var group in groupByMosid) {
                var mos = dbManager.GetByIdForce<MachineryOnShift>(group.First().MoSId);
                var order = mos.Order;

                // Группируем по Id машин, которые выбрали на замену
                var groupByMachine = group.GroupBy(x => x.MachineId);
                foreach (var a in groupByMachine) {
                    var machine = dbManager.GetById<Machinery>(a.First().MachineId);

                    // Находим машину на которую будем менять. Если машина с таким названием уже есть в наряде и она не удалена, то берем её, иначе создаём новую.
                    var newMos = order.Machineries.Any(x=> x.MachineryId == machine.Id && !x.IsDelete)? 
                        order.Machineries.First(x => x.MachineryId == machine.Id && !x.IsDelete) : 
                        new MachineryOnShift(machine);
                    mos.Delete(true);
                    newMos.GetAllParametres(mos);
                    foreach (var workid in a.Select(x => x.WorkId)) {
                        // Для каждой работы назначаем новую машину
                        var work = dbManager.GetByIdForce<Work>(workid);
                        work.SetParent(newMos);
                    }
                }
            }

            return true;
        }

        public MachineriesViewModel SaveOrUpdateMachinery(MachineryDTO dto) {

            var machine = dto.Id > 0 ? dbManager.GetById<Machinery>(dto.Id) : new Machinery();
            if (!String.IsNullOrEmpty(dto.Name) && machine.Name != dto.Name)
                machine.SetName(dto.Name);
            ChangeType(machine, dto.TypeId, out var conflictWorks);


            var model = GetMachineryViewModel(dto.Id);
            if (conflictWorks != null) {
                var moss = conflictWorks.Select(x => x.Parent).Distinct();
                var orders = moss.Select(x => x.Order).Distinct();
                foreach (var order in orders) {
                    if (model.Conflict == null)
                        model.Conflict = new List<ConflictOrders>();
                    model.Conflict.Add(new ConflictOrders(order));
                }
                foreach (var conflict in model.Conflict) {
                    if (conflict.FreeMachineries == null) conflict.FreeMachineries = new List<Machinery>();
                    foreach(var m in conflict.OrderVM.Machineries.Where(x => x.MachineryId != machine.Id).Select(x => x.MachineryId)) {
                        conflict.FreeMachineries.Add(dbManager.GetById<Machinery>(m));
                    }
                    var machines = orderManager.GetMachinesForOrder(conflict.Order);
                    foreach (var mach in machines) {
                        conflict.FreeMachineries.Add(mach);
                    }
                }
            }

            model.dto = dto;
            model.NewType = dbManager.GetById<MachineryType>(dto.TypeId);

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
