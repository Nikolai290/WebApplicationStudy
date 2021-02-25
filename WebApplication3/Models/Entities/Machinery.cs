
namespace WebApplication3.Models.Entities {
    public class Machinery : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual MachineryType Type { get; protected set; }

        public Machinery() { }
        public Machinery(string name, MachineryType type) {
            Name = name;
            Type = type;
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
