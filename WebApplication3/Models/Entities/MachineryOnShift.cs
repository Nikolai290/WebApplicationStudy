using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Entities {
    public class MachineryOnShift : Machinery {
        // DbEntities.Id
        // Machinerty.Name
        public virtual Order Order { get; protected set; }

        // Location
        public virtual QuarryArea Area { get; protected set; }
        public virtual QuarryField Field { get; protected set; }
        public virtual QuarryPlast Plast { get; protected set; }
        public virtual QuarryHorizon Horizon { get; protected set; }

        public virtual double Picket { get; protected set; } // (d2)
        
        
        // Group
        public virtual Group Group { get; protected set; }
        public virtual int Number { get; protected set; } // 1-999

        // Order
        public virtual double Weight { get; protected set; }
        public virtual double Volume { get; protected set; }
        public virtual double Overexcavation { get; protected set; }
        public virtual double Ash { get; protected set; }
        public virtual double Heat { get; protected set; }
        public virtual double Wet { get; protected set; }
        public virtual bool HighAsh { get; protected set; }

        // Downtime
        public virtual int TransportingTime { get; protected set; } // 0-24
        public virtual int ReapairingTime { get; protected set; } // 0-24
        public virtual int HolidayTime { get; protected set; } // 0-24

        // Crew
        public virtual IList<Employee> Crew { get; protected set; } // Max 10

        // PZO
        public virtual bool PZO { get; protected set; }


        public MachineryOnShift() { }




    }
}
