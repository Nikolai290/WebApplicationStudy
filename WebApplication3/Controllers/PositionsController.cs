using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;


namespace WebApplication3.Controllers {
    public class PositionsController : Controller {
        private IDbManager dbManager;

        private PositionManager positionManager;

        public PositionsController() {
            dbManager = new DbManager();
            positionManager = new PositionManager(dbManager);
        }

        public IActionResult Index() {

            ViewBag.Positions = positionManager.GetAllWithEmpls();
            dbManager.Commit();
            return View("Positions");
        }

        [HttpGet]
        public IActionResult Add(int id) {
            Position pos;
            if (id == 0) {
                pos = new Position();
            }
            else {
                pos = positionManager.GetById(id);
            }

            ViewBag.Positions = positionManager.GetDistinctNames();

            dbManager.Commit();
            return View("Add", pos);
        }

        [HttpPost]
        public IActionResult Add(Position pos, int id) {
            bool result;
            string message;
            pos.Check();

            if (id == 0)
                result = positionManager.Create(pos);
            else {
                var obj = positionManager.GetById(id);
                pos.CopyTo(obj);
                result = positionManager.Update(obj);
            }

            if (result) {
            message = "Объект сохранён";
            } else {
                message = "Не удалось";
            }

            dbManager.Commit();
            return View("Result", message);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            bool result = positionManager.Delete(id);
            string message;
            if (result) {
                message = "Объект удалён";
            } else {
                message = "Не удалось";
            }

            dbManager.Commit();
            return View("Result", message);
        }


    }
}
