using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services.EmployeeServices;

namespace WebApplication3.Models.Services.PositionServices {
    public class PositionManager {

        EmployeeManager employeeManager = new EmployeeManager();
        DbManager dbManager = new DbManager();


        public Position Compare(Position pos) {
            pos.Employees = employeeManager.GetEmployeeByPosition(pos);
            return pos;
        }

        public bool CreateNewPosition(Position pos) {
            var result = false;
            if (IsValid(pos, true))
                result = dbManager.Add(pos);
            return result;
        }

        public bool DeletePosition(Position pos) {

            var emps = employeeManager.GetAllEmployee().Where(x => x.Position.Id == pos.Id);
            foreach (var emp in emps) {
                emp.Position = null;
                employeeManager.UpdateEmployee(emp, emp.Id);
            }

            return dbManager.Delete(pos);
        }
        public bool DeletePosition(int id)
            => DeletePosition(GetPositionById(id));

        public IList<Position> GetAllPosition()
            => dbManager.GetAll<Position>();

        public IList<Position> GetAllPositionWithEmployees() {
            var poss = GetAllPosition();
            foreach (var pos in poss) {
                Compare(pos);
            }
            return poss;
        }

        public Position GetPosition(Employee emp)
           => GetAllPosition().Where(x => x.Id == emp.Position.Id).First();


        public Position GetPositionById(int id)
            => dbManager.GetById<Position>(id);

        public IList<Position> GetPositionByStringFind(string find)
            => GetAllPosition().Where(x => x.Subname.ToUpper().Contains(find.ToUpper())).ToList();

        public bool IsValid(Position obj, bool newPosition = true) {
            bool result = true;
            if (newPosition) {
                if (GetAllPosition().Where(x => x.Subname == obj.Subname && x.Name == obj.Name).Any()) {
                    result = false;
                }
            }
            if (StringsIsEmptyOrNull(obj))
                result = false;


            return result;
        }


        private bool StringsIsEmptyOrNull(Position pos)
            => (String.IsNullOrEmpty(pos.Subname) || String.IsNullOrEmpty(pos.Name));


        public bool UpdatePosition(Position pos, int id) {
            bool result = false;
            if (IsValid(pos, false)) {
                Position upd = GetPositionById(id);
                pos.CopyTo(upd);
                result = dbManager.Update(upd);
            }

            return result;
        }
    }
}
