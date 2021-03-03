using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.FluentValidation;

namespace WebApplication3.Models.Services {
    public class PositionManager {
        PositionsAddDTOValidator validator = new PositionsAddDTOValidator();

        private readonly IDbManager dbManager;

        public PositionManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool Create(Position pos) {
            if (IsValid(pos)) {
                return dbManager.AddAsync(pos);
            }
            return false;
        }

        public bool CreateNewPosition(PositionsAddDTO dto, out string message) {
            message = "";
            if (IsWriteProtection(dto.Id)) {
                message = "Запись защищена от изменений";
                return false;
            }

            var res = validator.Validate(dto);
            if(! res.IsValid) {
                message = res.ToString();
                return false;
            }

            Position pos;
            bool result;
            if (dto.Id > 0) {
                pos = GetById(dto.Id).Create(dto).Check();
                result = true;
            } else {
                pos = new Position().Create(dto).Check();
                result = Create(pos);
            }
            message = "Успешно!";
            return result;
        }

        internal PositionsAddDTO GetModelToAdd(PositionsAddDTO dto) {
            var model = GetModelToAdd(dto.Id);
            return model;
        }

        internal PositionsAddDTO GetModelToAdd(int id) {
            var model = new PositionsAddDTO();
            model.Position = id > 0 ?
                GetById(id) :
                new Position();
            model.Name = model.Position.Name;
            model.Subname = model.Position.Subname;

            model.Positions = GetDistinctNames();
            return model;
        }

        // Валидность
        private bool IsValid(Position pos, bool isNew = true) {
            if (isNew && IsAlreadyExist(pos)) return false;
            if (IsEmptyValue(pos)) return false;
            return true;
        }

        private static bool IsEmptyValue(Position pos)
            => (String.IsNullOrEmpty(pos.Name) && String.IsNullOrEmpty(pos.Subname));

        private bool IsAlreadyExist(Position pos)
            => (dbManager.GetAll<Position>().Where(x => x.Name == pos.Name && x.Subname == pos.Subname).Any());

        public bool PseudoDelete(int id) {
            return dbManager.PseudoDelete<Position>(id);
        }

        //public bool DeleteAsync(Position pos) {
        //     if (IsWriteProtection(pos.Id))
        //        return false;
        //    foreach (var emp in pos.Employees) {
        //        emp.Position = dbManager.GetById<Position>(1);
        //    }
        //    pos.Employees = null;
        //    return dbManager.DeleteAsync(pos);
        //}

        private static bool IsWriteProtection(int id)
            => (id == 1 || id == 2 || id == 3 || id == 4);

        //public bool DeleteAsync(int id)
        //    => DeleteAsync(GetById(id));

        public IQueryable<Position> GetAll()
            => dbManager.GetAll<Position>();

        public IList<PositionsViewModel> GetAllClean() {
            var model = new List<PositionsViewModel>();
            dbManager.GetAll<Position>().ToList().ForEach( x => model.Add(new PositionsViewModel(x)));
            model.ForEach(x => x.Employees = dbManager.GetAll<Employee>().Where(z => z.Position.Id == x.Id).ToList());
            return model;
        }

        public Position GetById(int id)
            => dbManager.GetById<Position>(id);


        public Position GetByIdAsync(int id)
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
