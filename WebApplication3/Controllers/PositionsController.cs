using Microsoft.AspNetCore.Mvc;
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

            return View("Add", pos);
        }

        [HttpPost]
        public IActionResult Add(Position pos) {
            bool result = positionManager.Create(pos);
            string message;
            if (result) {
            message = "Объект сохранён";
            } else {
                message = "Не удалось";
            }

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

            return View("Result", message);
        }


    }
}
