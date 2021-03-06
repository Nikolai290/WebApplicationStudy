﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Entities {
    public class MachineryOnShift : DbEntities {
        // DbEntities.Id
        public virtual string Name { get; protected set; }

        public virtual int MachineryId { get; protected set; } // for exclude repeat machinery
        public virtual Order Order { get; protected set; } // link

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
        public virtual int RepairingTime { get; protected set; } // 0-24
        public virtual int HolidayTime { get; protected set; } // 0-24

        // Crew
        public virtual IList<Employee> Crew { get; protected set; } // Max 10

        // PZO
        public virtual bool PZO { get; protected set; }

        // Works
        public virtual IList<Work> Works { get; protected set; } = new List<Work>();


        public MachineryOnShift() {
            Order = new Order();
            Area = new QuarryArea();
            Field = new QuarryField();
            Plast = new QuarryPlast();
            Horizon = new QuarryHorizon();
            Group = new Group();
            Crew = new List<Employee>();
            Works = new List<Work>();
        }
        public MachineryOnShift(Machinery m) : base() {
            MachineryId = m.Id;
            Name = m.Name;
        }
        public virtual void SetNulls() {
            Order.Machineries.Remove(this);
            Area.MachineryOnShift.Remove(this);
            Field.MachineryOnShift.Remove(this);
            Plast.MachineryOnShift.Remove(this);
            Horizon.MachineryOnShift.Remove(this);
            Group.MachineryOnShift.Remove(this);

            foreach (var emp in Crew)
                emp.Machinaries.Remove(this);

            Order = null;
            Area = null;
            Field = null;
            Plast = null;
            Horizon = null;
            Group = null;
            Crew = null;
        }

        public virtual MachineryOnShift GetAllParametres(MachineryOnShift source) {
            Order = source.Order;
            Area = source.Area;
            Field = source.Field;
            Plast = source.Plast;
            Horizon = source.Horizon;
            Picket = source.Picket;
            Group = source.Group;
            Number = source.Number;

            Weight = source.Weight;
            Volume = source.Volume;
            Overexcavation = source.Overexcavation;
            Ash = source.Ash;
            Heat = source.Heat;
            Wet = source.Wet;
            HighAsh = source.HighAsh;

            TransportingTime = source.TransportingTime;
            RepairingTime = source.RepairingTime;
            HolidayTime = source.HolidayTime;

            Crew = source.Crew.Where(x => !x.IsDelete).ToList();
            PZO = source.PZO;

            return this;
        }



        private static bool IsValidNumberGroupe(int value)
            => (value > 0 && value < 1000);

        public virtual MachineryOnShift AddEmployee(Employee empl) {
            if (Crew.Count < 10) {
                Crew.Add(empl);
            }

            return this;
        }

        public virtual MachineryOnShift SetCrew(IList<Employee> empls) {
            if (empls != null)
                Crew = empls.Take(10).ToList();
            return this;
        }


        public virtual MachineryOnShift SetLocation(QuarryArea area = null, QuarryField field = null, QuarryHorizon hor = null, QuarryPlast plast = null, double picket = 0) {
            Area = area;
            Field = field;
            Horizon = hor;
            Plast = plast;
            Picket = IsNotNegative(picket) ? picket : 0;

            return this;
        }

        public virtual MachineryOnShift SetGroup(Group group, int number) {
            Group = group;
            Number = IsValidNumberGroupe(number) ? number : 0;

            return this;
        }

        public virtual MachineryOnShift SetOrderProperties(double weight = 0, double volume = 0, double overex = 0, double ash = 0, double heat = 0, double wet = 0, bool highAsh = false) {
            Weight = IsNotNegative(weight) ? weight : 0;
            Volume = IsNotNegative(volume) ? volume : 0;
            Overexcavation = IsNotNegative(overex) ? overex : 0;
            Ash = IsNotNegative(ash) ? ash : 0;
            Heat = IsNotNegative(heat) ? heat : 0;
            Wet = IsNotNegative(wet) ? wet : 0;
            HighAsh = highAsh;

            return this;
        }

        public virtual MachineryOnShift SetDownTime(int trans, int repair, int holy) {
            TransportingTime = IsValidDownTime(trans) ? trans : 0;
            RepairingTime = IsValidDownTime(repair) ? repair : 0;
            HolidayTime = IsValidDownTime(holy) ? holy : 0;

            return this;
        }

        private bool IsValidDownTime(int num)
            => (num >= 0 && num <= 24);

        private static bool IsNotNegative(double n)
            => (n >= 0);


        public virtual MachineryOnShift SetPZO(bool pzo) {
            PZO = pzo;
            return this;
        }
        public virtual MachineryOnShift InversePZO() {
            PZO = !PZO;
            return this;
        }
        public virtual MachineryOnShift SetOrder(Order order) {
            Order = order;
            return this;
        }

        public virtual MachineryOnShift AddWork(Work work) {

            Works.Add(work);
            work.SetParent(this);
            return this;
        }

        public virtual MachineryOnShift SetWorks(IList<Work> works) {
            Works = works;
            works.ToList().ForEach(x => x.SetParent(this));
            return this;
        }

    }
}
