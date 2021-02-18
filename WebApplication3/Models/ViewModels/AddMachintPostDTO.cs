using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.ViewModels {
    public class AddMachintPostDTO {
        public virtual int OrderId { get; set; }
        public virtual int MachineId { get; set; }
        public virtual int MoSId { get; set; }
        public virtual int AreaId { get; set; }
        public virtual int FieldId{ get; set; }
        public virtual int PlastId { get; set; }
        public virtual int HorizonId { get; set; }
        public virtual int GroupId { get; set; }
        public virtual int[] Crew { get; set; }
        public virtual int Number { get; set; }
        public virtual int Transport { get; set; }
        public virtual int Repair { get; set; }
        public virtual int HoliDays { get; set; }
        //
        public virtual string HighAsh { get; set; }
        public virtual string PZO { get; set; }
        public virtual double Picket { get; set; }
        public virtual double Weight { get; set; }
        public virtual double Volume { get; set; }
        public virtual double Overex { get; set; }
        public virtual double Ash { get; set; }
        public virtual double Heat { get; set; }
        public virtual double Wet { get; set; }



    }
}
