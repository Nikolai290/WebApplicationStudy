using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class StaffAddDTO {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Fathername { get; set; }
        public virtual int TableNumber { get; set; }
        public virtual Position Position { get; set; }
        public virtual int PosId { get; set; }
    }
}
