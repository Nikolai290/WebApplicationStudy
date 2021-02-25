using System.Collections.Generic;

namespace WebApplication3.Models.ViewModels {
    public class MachineryTypeDTO {
        public int Id { get; set; }
        public string Name { get; protected set; }
        public IList<int> AreasId { get; set; }


        //
        //
    }
}
