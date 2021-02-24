using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.ViewModels {
    public class TimelineGetDTO {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public int ShiftId { get; set; }
        public int OrderAreaId { get; set; }
        public int MoSId{ get; set; }
        public int WorkId{ get; set; }

        public TimelineGetDTO() {
            Date = DateTime.Now.Date;
            ShiftId = 1;
            OrderAreaId = 1;

        }
    }
}
