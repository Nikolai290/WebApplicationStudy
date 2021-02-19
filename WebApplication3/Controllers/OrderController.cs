using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {


        private readonly IDbManager dbManager;
        private readonly OrderManager orderManager;


        public OrderController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);

        }

        [HttpGet]
        public async Task<IActionResult> Index(OrderGetDTO dto) {
            OrderIndexViewModel model = await orderManager.GetOrderIndexViewModel(dto);
            dbManager.Commit();
            return View("Order", model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderGetDTO dtoGet,OrderPostDTO dtoPost) {
            OrderIndexViewModel model = await orderManager.PostOrderIndexViewModel(dtoGet, dtoPost);
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
        public async Task<IActionResult> AddMachineToOrder(AddMachintPostDTO dto) {
            var result = await orderManager.AddNewMachineryOnShift(dto);
            dbManager.Commit();
            string message = result ? $"Объект сохранён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/order?orderIdforce={dto.OrderId}", "Назад");
            return View("Result", res);
        }

        [HttpGet]
        public IActionResult Delete(int mosId, int orderId) {
            var result = orderManager.DeleteMachineryOnShift(mosId);
            dbManager.Commit();
            string message = result ? $"Объект удалён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/order?orderIdforce={orderId}", "Назад");
            return View("Result", res);
        }

    }
}
