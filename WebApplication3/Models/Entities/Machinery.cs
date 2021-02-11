using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class Machinery : DbEntities {
        public virtual string Name { get; set; }

        public Machinery() { }
        public Machinery(string name) {
            Name = name;
        }


    }
}
