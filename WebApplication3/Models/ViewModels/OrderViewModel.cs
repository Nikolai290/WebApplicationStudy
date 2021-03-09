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
        public int DispetcherId { get; set; }
        public int ChiefId { get; set; }
        public IList<int> MiningMasterId { get; set; }

        // Добыча, вскрыша, автовсркыша
        public int AreaId { get; set; }

        public IList<int> MachineriesId { get; set; }

        public bool AllPzo { get; set; }

        public OrderViewModel(Order order) {
            Id = order.Id;
            IsDelete = order.IsDelete;
            Date = order.Date;
            Shift = order.Shift;
            IsClose = order.IsClose;
            DispetcherId = order.Dispetcher.Id; 
            ChiefId = order.Chief.Id;
            MiningMasterId = order.MiningMaster.Select(x => x.Id).ToList();
            AreaId = order.Area.Id;
            AllPzo = order.AllPzo;
            MachineriesId = order.Machineries.Where(x => !x.IsDelete).Select(x => x.Id).ToList();

        }
   }
}
