using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class CoalSort : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual IList<Work> Works { get; protected set; } = new List<Work>();

        public CoalSort() { }

        public CoalSort(string name) => Name = name;
    }
}
