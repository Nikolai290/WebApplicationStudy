using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.ViewModels {
    public class TimelineGetDTO {
        public virtual int OrderId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int Shift { get; set; }
        public virtual int OrderAreaId { get; set; }
        public virtual int MoSId{ get; set; }
        public virtual int WorkId{ get; set; }

        public TimelineGetDTO() {
            Date = DateTime.Now.Date;
            Shift = 1;
            OrderAreaId = 1;
        }
    }
}
