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

        public virtual string picket { get; set; }
        public virtual string weight { get; set; }
        public virtual string volume { get; set; }
        public virtual string overex { get; set; }
        public virtual string ash { get; set; }
        public virtual string heat { get; set; }
        public virtual string wet { get; set; }


        public AddMachintPostDTO ConvertToDouble() {

            Picket = double.TryParse(picket?.Replace('.', ','), out double pick)? pick: 0;
            Weight = double.TryParse(weight?.Replace('.',','), out double weight1)? weight1 : 0;
            Volume = double.TryParse(volume?.Replace('.', ','), out double vol)? vol : 0;
            Overex = double.TryParse(overex?.Replace('.', ','), out double over)? over : 0;
            Ash = double.TryParse(ash?.Replace('.', ','), out double ash1)? ash1 : 0;
            Heat = double.TryParse(heat?.Replace('.', ','), out double heat1) ? heat1 : 0;
            Wet = double.TryParse(wet?.Replace('.', ','), out double wet1)? wet1 : 0;
            return this;
        }
    }
}
