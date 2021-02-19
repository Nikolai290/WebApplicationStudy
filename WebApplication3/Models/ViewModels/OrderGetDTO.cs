using System;


namespace WebApplication3.Models.ViewModels {
    public class OrderGetDTO {
        public virtual int OrderIdForce { get; set; }
        public virtual int OrderAreaId { get; set; }
        public virtual int ShiftId { get; set; }
        public virtual DateTime Date { get; set; }




        public OrderGetDTO() {
            OrderAreaId = 1;
            ShiftId = 1;
            Date = DateTime.Now.Date;
        }
        
    }
}
