using System;

namespace WebApplication3.Models.Entities {
    public class Work : DbEntities {
        // dependence
        public virtual MachineryOnShift Parent { get; protected set; }
        //
        public virtual WorkTypes Type { get; protected set; }
        // base
        public virtual DateTime StartTime { get; protected set; }
        public virtual DateTime EndTime { get; protected set; }
        public virtual string Note { get; protected set; }
        //
        public virtual double Weight { get; protected set; }
        public virtual CoalSort Sort { get; protected set; }
        public virtual double Ash { get; protected set; }
        public virtual double Heat { get; protected set; }
        public virtual double Wet { get; protected set; }
        //
        public virtual int Wagons { get; protected set; }

        public Work() { }

        public virtual Work SetType(WorkTypes type) {
            Type = type;
            type.Works.Add(this);
            return this;
        }
        public virtual Work SetNote(string note) {
            Note = note;
            return this;
        }

        public virtual Work SetParent(MachineryOnShift parent) {
            Parent = parent;
            return this;
        }

        public virtual Work SetTime(DateTime start, DateTime end) {
            StartTime = start; // need external checking for right datetime
            EndTime = end;
            return this;
        }

        public virtual Work SetProperties(double weight, double ash, double heat, double wet, CoalSort sort, int wagons = 0) {
            Weight = weight > 0? weight: 0;
            Ash = ash > 0 ? ash: 0;
            Heat = heat > 0 ? heat: 0;
            Wet = wet > 0 ? wet: 0;
            Sort = sort;
            Wagons = wagons > 0? wagons: 0;
            sort.Works.Add(this);

            return this;
        }

    }
}
