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
        public virtual int TotalMinutes { get; protected set; }
        public virtual string Note { get; protected set; }
        //
        public virtual double Volume { get; protected set; }
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
            if(end > start) {
                StartTime = start; // need external checking for right datetime
                EndTime = end;
                TimeSpan delta = (end - start);
                TotalMinutes = Convert.ToInt32(delta.TotalMinutes);
            }

            
            return this;
        }

        public virtual Work SetVolume(double volume) {
            Volume = volume > 0 ? volume : 0;
            return this;
        }
        public virtual Work SetWagons(int wagons) {
            Wagons = wagons > 0 ? wagons : 0;
            return this;
        }

        public virtual Work SetProperties(double weight, double ash, double heat, double wet, CoalSort sort) {
            Weight = weight > 0? weight: 0;
            Ash = ash > 0 ? ash: 0;
            Heat = heat > 0 ? heat: 0;
            Wet = wet > 0 ? wet: 0;
            Sort = sort;
            sort.Works.Add(this);

            return this;
        }

    }
}
