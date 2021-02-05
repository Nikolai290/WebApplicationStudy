using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public abstract class Quarry : DbEntities {

        public virtual string Name { get; protected set; }

        public virtual IList<MachineryOnShift> MachineryOnShift { get; protected set; }

        public Quarry() { }
        public Quarry(string name) {
            Name = name;
        }

    }
}
