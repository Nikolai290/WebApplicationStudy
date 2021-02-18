using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;


namespace WebApplication3.Controllers {
    public class PositionsController : Controller {
        private IDbManager dbManager;

        private PositionManager positionManager;

        public PositionsController() {
            dbManager = new DbManager();
            positionManager = new PositionManager(dbManager);
        }

        public IActionResult Index() {

            var positions = positionManager.GetAll().ToList();
            dbManager.Commit();
            return View("Positions", positions);
        }

        [HttpGet]
        public IActionResult Add(int id) {
            var pos = id > 0 ? 
                positionManager.GetById(id) :
                new Position();

            ViewBag.Positions = positionManager.GetDistinctNames();

            dbManager.Commit();
            return View("Add", pos);
        }

        [HttpPost]
        public IActionResult Add(PositionsAddDTO pos) {
            var result = positionManager.CreateNewPosition(pos);
            string message = result ? "Объект сохранён" : "Не удалось";
            dbManager.Commit();
            return View("Result", message);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            bool result = positionManager.Delete(id);
            string message = result ? "Объект удалён" : "Не удалось";
            dbManager.Commit();
            return View("Result", message);
        }


    }
}
