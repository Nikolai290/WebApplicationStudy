using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication3.Models.ViewModels {
    public class AddMachineGetDTO {
        public virtual int OrderId { get; set; }
        public virtual int MachineId { get; set; }
        public virtual int MoSId { get; set; }
        //mosId - MachineryOnShiftId


    }
}
