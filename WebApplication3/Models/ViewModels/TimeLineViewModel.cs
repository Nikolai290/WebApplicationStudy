using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class TimeLineViewModel {
        public virtual Order Order { get; set; }
        public virtual Work Work { get; set; }
        public virtual string MachineName { get; set; }
        public virtual IList<WorkTypes> WorkTypes { get; set; }
        public virtual IList<CoalSort> CoalSorts { get; set; }
        public virtual IList<OrderArea> Areas { get; set; }
        public virtual IList<string> Hours { get; set; }



    }
}
