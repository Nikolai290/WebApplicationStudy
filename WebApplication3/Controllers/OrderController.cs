using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {


        private IDbManager dbManager;
        private OrderManager orderManager;


        public OrderController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);

        }

        [HttpGet]
        public IActionResult Index(OrderGetDTO dto) {

            OrderIndexViewModel model = orderManager.GetOrderIndexViewModel(dto);
            dbManager.Commit();
            return View("Order", model);
        }

        [HttpPost]
        public IActionResult Index(OrderGetDTO dtoGet,OrderPostDTO dtoPost) {

            OrderIndexViewModel model = orderManager.PostOrderIndexViewModel(dtoGet, dtoPost);
            dbManager.Commit();

            return View("Order", model);
        }


 
        [HttpGet]
        public IActionResult AddMachineToOrder(AddMachineGetDTO dtoGet) {


            var model = orderManager.GetAddingMachineViewModel(dtoGet);

            dbManager.Commit();
            return View("AddMachineToOrder", model);
        }


        [HttpPost]
        public IActionResult AddMachineToOrder(AddMachintPostDTO dto) {
            var result = orderManager.AddNewMachineryOnShift(dto);
            dbManager.Commit();
            string message = result ? $"Объект сохранён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/order?orderId={dto.OrderId}", "Назад");
            return View("Result", res);
        }

        [HttpGet]
        public IActionResult Delete(int mosId, int orderId) {
            var result = orderManager.DeleteMachineryOnShift(mosId);
            dbManager.Commit();
            string message = result ? $"Объект удалён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/order?orderId={orderId}", "Назад");
            return View("Result", res);
        }

    }
}
