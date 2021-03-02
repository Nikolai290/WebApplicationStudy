using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class MachineriesViewModel {
        public IList<Machinery> Machineries { get; set; }
        public IList<MachineryType> Types { get; set; }
        
        public Machinery Machine { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }

        public MachineryType NewType { get; set; }
        public IList<ConflictOrders> ConflictOrders { get; set; }
        public IList<Work> ConflictWorks { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
    }
}
