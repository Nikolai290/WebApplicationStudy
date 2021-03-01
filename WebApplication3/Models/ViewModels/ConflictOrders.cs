using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class ConflictOrders {
        public Order Order { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public IList<Machinery> FreeMachineries { get; set; }


        public ConflictOrders (Order order) {
            Order = order;
            OrderVM = new OrderViewModel(order);
        }
    }
}
