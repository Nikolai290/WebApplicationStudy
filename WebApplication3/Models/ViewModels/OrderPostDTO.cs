using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.ViewModels {
    public class OrderPostDTO {
        public virtual int OrderId { get; set; }
        public virtual int DispetcherId { get; set; }
        public virtual int ChiefId { get; set; }
        public virtual int[] MastersId { get; set; }
    }
}
