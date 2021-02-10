using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Services;
using WebApplication3.Models.DbAccess;


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
            return View("Result", "База успешно инициализрована!");

        }

    }
}
