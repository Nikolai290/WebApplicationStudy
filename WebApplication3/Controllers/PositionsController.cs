using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services.PositionServices;
using WebApplication3.Models.Services.EmployeeServices;


namespace WebApplication3.Controllers {
    public class PositionsController : Controller {

        PositionManager positionManager = new PositionManager();

        // Зависает намертво, скорее всего где-то здесь или в PositionManager ошибка

        public IActionResult Index() {

            ViewBag.Positions = positionManager.GetAllPositionWithEmployees();

            return View("Positions");
        }

        [HttpGet]
        public IActionResult Add(int id) {
            Position pos;
            if (id == 0) {
                pos = new Position();
            }
            else {
                pos = positionManager.GetPositionById(id);
            }
            var list = positionManager.GetAllPosition();
            var arr = list.OrderBy(x => x.Name).Select(x => x.Name).Distinct();

            ViewBag.Positions = arr;

            return View("Add", pos);
        }

        [HttpPost]
        public IActionResult Add(Position pos) {
            bool result = positionManager.CreateNewPosition(pos);
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
            bool result = positionManager.DeletePosition(id);
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
