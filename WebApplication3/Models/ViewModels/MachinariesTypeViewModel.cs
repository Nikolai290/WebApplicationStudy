using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class MachinariesTypeViewModel {

        public IList<MachineryType> Types { get; set; }
        public IList<OrderArea> Areas { get; set; }
        public MachineryType Type { get; set; }

        public string Message { get; set; } = "";
    }
}
