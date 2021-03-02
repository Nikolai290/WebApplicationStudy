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
            var check = valid.CheckMachineryDTO(dto, out string message);
            IList<ConflictOrders> conflict = null;
            MachineryType newType = null;
            if (check) {
                var machine = dto.Id > 0 ? dbManager.GetById<Machinery>(dto.Id) : new Machinery();
                machine.SetName(dto.Name);
                ChangeType(machine, dto.TypeId, out conflict, out message, out newType);
                if (dto.Id == 0) dbManager.AddAsync(machine);
            }
            var model = GetMachineryViewModel(dto.Id);
            model.ConflictOrders = conflict;
            model.Message = message;
            model.NewType = newType;

            return model;
        }

        internal void SolveConflict(IList<SolveConflictDTO> dto) {
            /*
             * В случае конфликта нам необходимо получить из представления следующие данные:
             * Массив данных содержащий:
             * Id заменённого (нового) оборудования
             * Id работы для которой производится замена
             * Остальные данные находим из работы: Id ордера и Id заменяемого оборудования
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
                foreach(var z in x) {

                }
            }

            foreach (var c in dto ){

                var work = dbManager.GetById<Work>(c.WorkId);
                var mos = work.Parent;
                var order = mos.Order;
                // mos?.Delete(true);
                var machine = dbManager.GetById<Machinery>(c.MachineId);




            }



        }

        private Machinery ChangeType(Machinery machine, int typeId, out IList<ConflictOrders> conflictOrders, out string message, out MachineryType newType) {
            conflictOrders = new List<ConflictOrders>();
            message = "Успешно";
            newType = dbManager.GetById<MachineryType>(typeId);
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
                foreach (var order in orders)
                    conflictOrders.Add(new ConflictOrders(order));
                foreach (var c in conflictOrders)
                    c.FreeMachineries = orderManager.GetMachinesForOrder(c.Order);

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
