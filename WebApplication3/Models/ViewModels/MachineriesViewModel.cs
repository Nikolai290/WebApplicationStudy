using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class MachineriesViewModel {
        public IList<Machinery> Machineries { get; set; }
        public IList<MachineryType> Types { get; set; }
        public Machinery Machine { get; set; }
    }
}
