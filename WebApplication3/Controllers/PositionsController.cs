using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.FluentValidation;


namespace WebApplication3.Controllers {
    public class PositionsController : Controller {
        private readonly IDbManager dbManager;

        private readonly PositionManager positionManager;
        private readonly PositionValidator validator;

        public PositionsController() {
            dbManager = new DbManager();
            positionManager = new PositionManager(dbManager);
            validator = new PositionValidator();
        }

        public IActionResult Index() {

            var positions = positionManager.GetAllClean();
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
        public IActionResult Add(Position dto) {
            
            if (!ModelState.IsValid) {
                ViewBag.Positions = positionManager.GetDistinctNames();

                dbManager.Commit();
                return View("Add", dto);
            }
            // var result = positionManager.CreateNewPosition(dto, out string message);
            dbManager.Commit();
            return BadRequest("Успешно!");

            // string message = result ? $"Объект сохранён" : "Не удалось";
            // var res = new ResultViewModel(message, message, $"/positions", "Назад");
            return View("Result");
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            bool result = positionManager.PseudoDelete(id);
            dbManager.Commit();

            string message = result ? $"Объект удалён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/positions", "Назад");
            return View("Result", res);
        }


    }
}
