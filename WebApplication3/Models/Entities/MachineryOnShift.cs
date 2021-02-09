using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Entities {
    public class MachineryOnShift : Machinery {
        // DbEntities.Id
        // Machinerty.Name
        public virtual int MachineryId { get; protected set; } // for exclude repeat machinery
        public virtual Order Order { get; protected set; } // link

        // Location
        public virtual QuarryArea Area { get; set; }
        public virtual QuarryField Field { get; set; }
        public virtual QuarryPlast Plast { get; set; }
        public virtual QuarryHorizon Horizon { get; set; }

        public virtual double Picket { get; set; } // (d2)


        // Group
        public virtual Group Group { get; set; }
        public virtual int Number { // 1-999
            get => Number;
            set {
                if (IsValid(value))
                    Number = value;
            }
        }



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
        public virtual bool PZO { get; set; }


        public MachineryOnShift() {

        }


        private bool IsValid(int value)
            => (value > 0 && value < 1000);

        public virtual MachineryOnShift AddEmployee(Employee empl) {
            if (Crew.Count < 10) {
                Crew.Add(empl);
            }

            return this;
        }

        public virtual Machinery SetCrew(List<Employee> empls) {
            Crew = empls;
            return this;
        }


    }
}
