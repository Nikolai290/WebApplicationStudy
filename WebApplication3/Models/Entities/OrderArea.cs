using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class OrderArea : DbEntities {

        public virtual string Name { get; protected set; }
        public virtual IList<Order> Orders { get; protected set; }

        public OrderArea() { }
        public OrderArea(string name) {
            Name = name;
        }

    }
}
