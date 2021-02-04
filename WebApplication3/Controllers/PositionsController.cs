using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services.PositionServices;


namespace WebApplication3.Controllers {
    public class PositionsController : Controller {

        PositionManager positionManager = new PositionManager();


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
