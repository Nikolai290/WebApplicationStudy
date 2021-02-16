using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class Figure : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual IList<WorkTypes> Type { get; protected set; } = new List<WorkTypes>();

        public Figure() { }

        public Figure(string name) => Name = name;
    }
}
