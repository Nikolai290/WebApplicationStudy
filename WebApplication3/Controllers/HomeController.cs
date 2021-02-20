using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Services;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;


namespace WebApplication3.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }
        public IActionResult Initialize() {
            new DbManager().Commit();
            new InitializeDb().Start();
            string message = "База успешно инициализиорована";
            var res = new ResultViewModel(message, message, $"/staff", "Назад");

            return View("Result", res);

        }

    }
}
