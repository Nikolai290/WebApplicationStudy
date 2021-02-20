using System.Collections.Generic;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.ViewModels {
    public class PositionsViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public IList<Employee> Employees { get; set; }

        public PositionsViewModel(Position pos) {
            Id = pos.Id;
            Name = pos.Name;
            Subname = pos.Subname;
            Employees = pos.Employees;
        }
    }
}
