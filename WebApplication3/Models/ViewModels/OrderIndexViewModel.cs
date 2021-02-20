using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class OrderIndexViewModel {

        public virtual Order Order { get; set; }
        public virtual string Next { get; set; }
        public virtual string Previos { get; set; }
        public virtual IList<OrderArea> Areas { get; set; }
        public virtual IList<Employee> Dispetchers { get; set; }
        public virtual IList<Employee> Chiefs { get; set; }
        public virtual IList<Employee> MiningMasters { get; set; }
        public virtual IList<Machinery> Machines { get; set; }



    }
}
