using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class PositionManager {

        private IDbManager dbManager;
        //private EmployeeManager employeeManager;

        public PositionManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            //employeeManager = new EmployeeManager(dbManager);
        }


        // Команды
        public bool Create(Position pos) {
            if (IsValid(pos)) {
                return dbManager.Add<Position>(pos);
            }
            return false;
        }


        // Валидность
        private bool IsValid(Position pos, bool isNew = true) {

            if (isNew && IsAlreadyExist(pos)) return false;
            if (IsEmptyValue(pos)) return false;

            return true;
        }

        private bool IsEmptyValue(Position pos)
            => (String.IsNullOrEmpty(pos.Name) && String.IsNullOrEmpty(pos.Subname));

        private bool IsAlreadyExist(Position pos)
            => (dbManager.GetAll<Position>().Where(x => x.Name == pos.Name && x.Subname == pos.Subname).Any());

        // Команды
        public bool Update(Position pos) {
            if (pos.Id == 1)
                return false;

            var result = true;
            if (IsValid(pos, false)) {
                result = dbManager.Update(pos);
            }
            return result;
        }

        public bool Delete(Position pos) {
            if (pos.Id == 1 || pos.Id == 2 || pos.Id == 3 || pos.Id == 4)
                return false;

            foreach (var emp in pos.Employees) {
                emp.Position = dbManager.GetById<Position>(1);
            }
            pos.Employees = null;

            return dbManager.Delete(pos);
        }

        public bool Delete(int id)
            => Delete(GetById(id));

        // Запросы
        public IList<Position> GetAll()
            => dbManager.GetAll<Position>().ToList();

        public Position GetById(int id)
            => GetAll().Where(x => x.Id == id).First();

        public IList<Position> Find(string find)
            => GetAll().Where(x => x.ToString().ToUpper().Contains(find.ToUpper())).ToList();

        public IList<Position> GetAllWithEmpls() {
            var poss = GetAll();
            
            //foreach (var pos in poss)
            //    Compare(pos);
            return poss;
        }
        //public Position Compare(Position pos) {
        //    pos.Employees = employeeManager.GetEmployeeByPosition(pos);
        //    return pos;
        //}

        public IList<string> GetDistinctNames() {

            var all = GetAll().OrderBy(x => x.Name).Select(x => x.Name).ToArray();
            IList<string> result = new List<string>();
            foreach (var line in all) {
                if (!result.Contains(line))
                    result.Add(line);
            }

            return result;
        }




    }
}
