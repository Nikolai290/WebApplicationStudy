using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Entities {
    public class MachineryType : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual IList<Machinery> Machineries  { get; protected set; }
        public virtual IList<OrderArea> Areas  { get; protected set; }
        
        public MachineryType() { }

        public virtual MachineryType SetName(string name) {
            Name = name;
            return this;
        }

        public virtual MachineryType SetArea(IList<OrderArea> area) {
            Areas = area;
            return this;
        }
    }
}
