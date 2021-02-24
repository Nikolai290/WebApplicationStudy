using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class TimeLineViewModel {
        public Order Order { get; set; }
        public Work Work { get; set; }
        public string MachineName { get; set; }
        public IList<WorkTypes> WorkTypes { get; set; }
        public IList<CoalSort> CoalSorts { get; set; }
        public IList<OrderArea> Areas { get; set; }
        public IList<string> Hours { get; set; }
        public IList<string> TimeIntervals { get; set; }


    }
}
