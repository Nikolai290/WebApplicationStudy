using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services.EmployeeServices;

namespace WebApplication3.Models.Services.PositionServices {
    public class PositionManager {
        EmployeeManager employeeManager = new EmployeeManager();

        DbManager dbManager = new DbManager();

        // Команды
        public bool Create(Position pos) {
            if (IsValid(pos)) {
                return dbManager.Add<Position>(pos);
            }
            return false;
        }


        // Валидность
        private bool IsValid (Position pos, bool isNew=true) {

            if (isNew && IsAlreadyExist(pos)) return false;
            if (IsEmptyValue(pos)) return false;

            return true;
        }

        private bool IsEmptyValue(Position pos)
            => (String.IsNullOrEmpty(pos.Name) && String.IsNullOrEmpty(pos.Subname));

        private bool IsAlreadyExist(Position pos)
            => (dbManager.GetAll<Position>().Where(x=> x.Name==pos.Name && x.Subname==pos.Subname).Any());
        //
        // Команды
        public bool Update(Position pos) {
            if (IsValid(pos)) {
                return dbManager.Update<Position>(pos);
            }
            return false;
        }

        public bool Delete(Position pos) {

            var emps = employeeManager.GetEmployeeByPosition(pos);
            foreach (var emp in emps) {
                emp.Position = null;
                employeeManager.UpdateEmployee(emp, emp.Id);
            }

            return dbManager.Delete<Position>(pos); 
        }

        public bool Delete(int id)
            => Delete(GetById(id));

        // Запросы
        public IList<Position> GetAll() 
            => dbManager.GetAll<Position>().ToList();

        public Position GetById(int id)
            => GetAll().Where(x => x.Id == id).First();

        public IList<Position> Find(string find) 
            => GetAll().Where(x => x.Subname.ToUpper().Contains(find.ToUpper())).ToList();
        
        public IList<Position> GetAllWithEmpls() {
            var poss = GetAll();

            foreach (var pos in poss)
                Compare(pos);
            return poss;
        }
        public Position Compare(Position pos) {
            pos.Employees = employeeManager.GetEmployeeByPosition(pos);
            return pos;
        }

        public IList<string> GetDistinctNames() 
            => GetAll().OrderBy(x => x.Name).Select(x => x.Name).Distinct().ToArray();
        


        
    }
}
