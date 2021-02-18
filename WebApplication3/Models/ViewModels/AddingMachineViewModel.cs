using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class AddingMachineViewModel {

        public virtual MachineryOnShift MachineryOnShift { get; set; }
        public virtual int OrderId { get; set; }
        public virtual IList<QuarryArea> Areas { get; set; }
        public virtual IList<QuarryField> Fields { get; set; }
        public virtual IList<QuarryHorizon> Horizons { get; set; }
        public virtual IList<Group> Groups { get; set; }
        public virtual IList<QuarryPlast> Plasts { get; set; }
        public virtual IList<Employee> FreeDrivers { get; set; }
 



    }
}
