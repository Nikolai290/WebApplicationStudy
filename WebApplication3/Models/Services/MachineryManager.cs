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


            IList<ConflictOrders> conflictOrders = null;
            IList<Work> conflictWorks = null;
            MachineryType newType = null;
            string message ="";
            
            {
                var machine = dto.Id > 0 ? dbManager.GetById<Machinery>(dto.Id) : new Machinery();
                if(!String.IsNullOrEmpty(dto.Name)) machine.SetName(dto.Name);
                ChangeType(machine, dto.TypeId, out conflictOrders, out conflictWorks, out message, out newType);
                if (dto.Id == 0) dbManager.AddAsync(machine);
            }

            var model = GetMachineryViewModel(dto.Id);
            model.ConflictWorks = conflictWorks;
            model.ConflictOrders = conflictOrders;
            model.Message = message;
            model.NewType = newType;
            model.Id = dto.Id;
            model.Name = dto.Name;
            model.TypeId = dto.TypeId;

            return model;
        }

        internal void SolveConflict(IList<SolveConflictDTO> dto) {
            /*
             * В случае конфликта нам необходимо получить из представления следующие данные:
             * Массив данных содержащий:
             * Id заменяемого оборудования
             * Id заменённого (нового) оборудования
             * Id работы для которой производится замена
             * Id наряда
             * Заменяемое оборудование подлежит псевдо удалению. Все данные из него копируются в новое оборудование
             * В изменяемой работе меняется только Id оборудования
              */
            /*
             * Варианты событий:
             * Для каждой работы назначается разная техника - самый простой вариант
             * Для некоторых работ в наряде удаляемого оборудования назначется одинаковая техника
             *  В этом случае необходимо это предусмотерть
             * Для всех работ в наряде удаляемого оборудования назначается одна новая техника
             */
            var m = dto.GroupBy(x => x.MoSId);

            foreach (var x in m) {
                var mosid = x.First().MoSId;
                foreach (var z in x.GroupBy(c => c.MachineId)) {
                    var machinid = z.First().MachineId;
                    var worksid = z.Select(b => b.WorkId).ToList();
                    ResetMachineryWork(mosid, machinid, worksid);
                }
            }
        }

        private void ResetMachineryWork(int mosId, int machineId, IList<int> worksId) {
            var machine = dbManager.GetById<Machinery>(machineId);
            var mos = dbManager.GetByIdForce<MachineryOnShift>(mosId);
            var newMos = new MachineryOnShift(machine).GetAllParametres(mos);
            foreach (int wId in worksId) {
                var work = dbManager.GetById<Work>(wId);
                work.SetParent(newMos);
                newMos.AddWork(work);
            }
            mos.Delete(true);
        }
        private Machinery ChangeType(Machinery machine, int typeId, out IList<ConflictOrders> conflictOrders, out IList<Work> conflictWorks, out string message, out MachineryType newType) {
            conflictOrders = new List<ConflictOrders>();
            message = "Успешно";
            newType = dbManager.GetById<MachineryType>(typeId);
            conflictWorks = new List<Work>();
            if (machine.Type.Id == typeId) {
                return machine;
            }
            if (newType.Areas.Any(x => machine.Type.Areas.Any(z => z.Id == x.Id))) {
                machine.SetType(newType);
                return machine;
            } else {
                // var changhes = dbManager.GetAll<MachineryOnShift>().Where(x => x.MachineryId == machine.Id);
                // Поиск конфликтов

                var orders = dbManager.GetAll<Order>().Where(x => x.Machineries.Any(z => !z.IsDelete && z.MachineryId == machine.Id)).ToList();
                var mos = new List<MachineryOnShift>(); 
                var works = new List<Work>();

                orders.ForEach(x => mos.AddRange(x.Machineries));
                mos.ForEach(x => works.AddRange(x.Works.Where(z => !z.IsDelete)));
                conflictWorks = works;
                foreach (var order in orders)
                    conflictOrders.Add(new ConflictOrders(order));
                
                foreach (var c in conflictOrders) {

                    c.FreeMachineries = orderManager.GetMachinesForOrder(dbManager.GetById<Order>(c.OrderVM.Id));
                }

                message = "Необходимо устранить конфликт";
                return machine;
            }
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
