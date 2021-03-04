using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Entities {
    public class MachineryType : DbEntities {
        public virtual string Name { get; set; }
        public virtual IList<Machinery> Machineries { get; protected set; }
        public virtual IList<OrderArea> Areas { get; set; }
        
        public MachineryType() {
            Machineries = new List<Machinery>();
            Areas = new List<OrderArea>();
        }
        public MachineryType(string name, params OrderArea[] areas) {
            Name = name;
            Areas = areas;
        }

        public virtual MachineryType SetName(string name) {
            Name = name;
            return this;
        }

        public virtual MachineryType SetArea(IList<OrderArea> area) {
            Areas = area;
            return this;
        }

        public virtual string GetAreasNames() {
            string res = "";
            foreach (var name in Areas.Select(x => x.Name))
                res += name + " ";
            return res;
        }
    }
}
