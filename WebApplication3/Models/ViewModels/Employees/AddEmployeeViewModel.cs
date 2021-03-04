using System.Collections.Generic;
using WebApplication3.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models.ViewModels.Employees {
    public class AddEmployeeViewModel {

        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(30,ErrorMessage = "Длинна строки {0}  должна быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Фамилия:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Длинна строки {0}  должна быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Имя:")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Длинна строки {0}  должна быть от {2} до {1} символов", MinimumLength = 1)]
        [Display(Name = "Отчество:")]
        public string Fathername { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Range(1000,999999, ErrorMessage = "{0} должен содержать от 4 до 6 чисел")]
        [Display(Name = "Табельный номер:")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Выберите должность сотрудника")]
        [Range(2,maximum: 2147483647, ErrorMessage = "Выберите должность сотрудника")]
        [Display(Name = "Должность:")]

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public IList<Position> Positions;


        public void CopyFrom(Employee emp) {
            Id = emp.Id;
            Name = emp.Name;
            Lastname = emp.Lastname;
            Fathername = emp.Fathername;
            TableNumber = emp.TableNumber;
            Position = emp.Position;
        }
    }
}
