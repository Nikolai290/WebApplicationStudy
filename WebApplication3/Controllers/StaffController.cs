using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;
using WebApplication3.Models.ViewModels;


namespace WebApplication3.Controllers {
    public class StaffController : Controller {

        private readonly IDbManager dbManager;
        private readonly PositionManager positionManager;
        private readonly EmployeeManager employeeManager;

        public StaffController() {
            dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
            positionManager = new PositionManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(string find) {

            var empls = String.IsNullOrEmpty(find) ? 
                employeeManager.GetAll().ToList() :
                employeeManager.GetEmployeesByStringFind(find);

            dbManager.Commit();
            return View("Staff", empls);
        }


        [HttpGet]
        public IActionResult Add(int id) {
            ViewBag.Positions = positionManager.GetAll().ToList();
            Employee emp = id > 0 ?
                employeeManager.GetById(id) :
                new Employee();

            dbManager.Commit();
            return View("Add", emp);
        }


        [HttpPost]
        public async Task<IActionResult> Add(StaffAddDTO emp) {


            var result = await employeeManager.AddAsync(emp);

            //string message = result ? "Объект сохранён" : "Не удалось";

            dbManager.Commit();
            string message = result != null ? $"Объект сохранён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/staff", "Назад");

            return View("Result", res);
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            var result = employeeManager.DeleteAsync(id);
            dbManager.Commit();
            string message = result ? $"Объект удалён" : "Не удалось";
            var res = new ResultViewModel(message, message, $"/staff", "Назад");

            return View("Result", res);
        }

    }

}
