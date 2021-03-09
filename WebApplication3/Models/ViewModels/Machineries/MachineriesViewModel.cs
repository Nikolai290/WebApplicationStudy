using System.Collections.Generic;
using WebApplication3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models.ViewModels.Machineries {
    public class MachineriesViewModel {

        // to do: decompose viewModel for different actions

        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название оборудования")]
        [StringLength(50, ErrorMessage = "Строка {0} должна содержать от {2} до {1} символов", MinimumLength = 3)]
        public string Name { get; set; }
        public int TypeId { get; set; }
        
        [Required(ErrorMessage = "Выберите тип")]
        [Range(1, 2147483647, ErrorMessage = "Выберите тип")]
        public int NewTypeId { get; set; }
        public string TypeName { get; set; }

        public IList<Machinery> Machineries { get; set; }
        public IList<MachineryType> Types { get; set; }

        public string Message { get; set; }
        public string Title { get; set; }

        public MachineryType NewType { get; set; }
        public IList<ConflictOrders> Conflict { get; set; }

        public void CopyFrom(Machinery machine) {
            Id = machine.Id;
            Name = machine.Name;
            if (machine.Type != null) {
                TypeId = machine.Type.Id;
                TypeName = machine.Type.Name;
            }

        }
    }
}
