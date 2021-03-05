using System.Collections.Generic;
using WebApplication3.Models.Entities;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models.ViewModels.Machineries {
    public class MachinariesTypeViewModel {


        public int Id { get; set; }
        
        [Required(ErrorMessage = "Введите название типа")]
        [StringLength(30, ErrorMessage = "Длинна строки {0}  должна быть от {2} до {1} символов", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите участок из списка")]
        [MinLength(1,ErrorMessage = "Выберите участок из списка")]
        public IList<int> AreasId { get; set; } = new List<int>();


        public IList<MachineryType> Types { get; set; } 
        public IList<OrderArea> Areas { get; set; } 
        public string Message { get; set; }
        public string Title { get; set; }


        public MachinariesTypeViewModel() {
            Types = new List<MachineryType>();
            Areas = new List<OrderArea>();
        }

        public void CopyFrom(MachineryType type) {
            Id = type.Id;
            Name = type.Name;
            AreasId = type.Areas.Select(x => x.Id).ToList();
        }
    }
}
