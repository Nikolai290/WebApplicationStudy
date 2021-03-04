using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.FluentValidation;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;
using WebApplication3.Models.ViewModels.Positions;



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
            var model = positionManager.GetModelToAdd(id);

            dbManager.Commit();
            return View("Add", model);
        }


        [HttpPost]
        public IActionResult Add(PositionsViewModel dto) {
            if (!ModelState.IsValid) {
                dto.Positions = positionManager.GetDistinctNames();
                dbManager.Commit();
                return View("Add", dto);
            }
            positionManager.CreateNewPosition(dto, out string message);
            dbManager.Commit();

            var res = new ResultViewModel(message, message, $"/positions", "Назад");
            return View("Result", res);
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
