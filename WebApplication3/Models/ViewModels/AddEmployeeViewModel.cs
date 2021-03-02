using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class AddEmployeeViewModel {
        public Employee Emp;
        public IList<Position> Positions;
    }
}
