using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class PositionManager {

        private IDbManager dbManager;

        public PositionManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }


        // Команды
        public bool Create(Position pos) {
            if (IsValid(pos)) {
                return dbManager.Add<Position>(pos);
            }
            return false;
        }


        public bool CreateNewPosition(PositionsAddDTO model) {
            if (IsWriteProtection(model.Id))
                return false;
            bool result = false;
            Position pos;
            if(model.Id > 0) {
                pos = GetById(model.Id).Create(model).Check();
                result = true;
            } else {
                pos = new Position().Create(model).Check();
                result = Create(pos);
            }

            return result;
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

        public bool Delete(Position pos) {
             if (IsWriteProtection(pos.Id))
                return false;
            
            foreach (var emp in pos.Employees) {
                emp.Position = dbManager.GetById<Position>(1);
            }
            pos.Employees = null;

            return dbManager.Delete(pos);
        }

        private bool IsWriteProtection(int id)
            => (id == 1 || id == 2 || id == 3 || id == 4);

        public bool Delete(int id)
            => Delete(GetById(id));

        // Запросы
        public IQueryable<Position> GetAll()
            => dbManager.GetAll<Position>();

        public Position GetById(int id)
            => dbManager.GetById<Position>(id);

        public IList<string> GetDistinctNames() {

            var all = GetAll().Select(x => x.Name).ToArray();
            var result = new List<string>();
            foreach (var line in all) {
                if (!result.Contains(line))
                    result.Add(line);
            }

            return result;
        }




    }
}
