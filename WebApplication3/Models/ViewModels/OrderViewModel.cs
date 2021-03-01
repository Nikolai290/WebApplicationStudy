using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class OrderViewModel {
        // Этот класс - копия класса Order
        // Но у этого класса все поля открыты для изменений
        // Объекты этого класса будут создаваться на основе объектов оригинального класса Order
        // Отличие в том, что в этом классе не будет содержаться никаких удалённых объектов Machinaries, Works и т.п.

        // Общие
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public bool IsClose { get; set; }
        // Персонал
        public Employee Dispetcher { get; set; }
        public Employee Chief { get; set; }
        public IList<Employee> MiningMaster { get; set; }

        // Добыча, вскрыша, автовсркыша
        public OrderArea Area { get; set; }

        public IList<MachineryOnShift> Machineries { get; set; }

        public bool AllPzo { get; set; }

        public OrderViewModel(Order order) {
            Id = order.Id;
            IsDelete = order.IsDelete;
            Date = order.Date;
            Shift = order.Shift;
            IsClose = order.IsClose;
            Dispetcher = order.Dispetcher; //?
            Chief = order.Chief;
            MiningMaster = order.MiningMaster;
            Area = order.Area;
            AllPzo = order.AllPzo;
            Machineries = order.Machineries.Where(x => !x.IsDelete).ToList();
            foreach (var m in Machineries) {
                m.SetWorks(m.Works.Where(x => !x.IsDelete).ToList());
            }
        }
   }
}
