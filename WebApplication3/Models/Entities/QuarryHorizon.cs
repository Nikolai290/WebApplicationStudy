using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class QuarryHorizon : DbEntities {

        public virtual string Name { get; protected set; }

        public virtual IList<MachineryOnShift> MachineryOnShift { get; protected set; }

        public QuarryHorizon() { }
        public QuarryHorizon(string name) {
            Name = name;
        }
    }
}
