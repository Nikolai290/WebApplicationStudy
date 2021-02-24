using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class Machinery : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual MachineryType Type { get; protected set; }

        public Machinery() { }
        public Machinery(string name) {
            Name = name;
        }
        public virtual Machinery SetName(string name) {
            Name = name;
            return this;
        }

        public virtual Machinery SetType(MachineryType type) {
            Type = type;
            return this;
        }


    }
}
