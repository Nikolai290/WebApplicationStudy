using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class MachineriesViewModel {
        public IList<Machinery> Machineries { get; set; }
        public IList<MachineryType> Types { get; set; }
        public Machinery Machine { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
    }
}
