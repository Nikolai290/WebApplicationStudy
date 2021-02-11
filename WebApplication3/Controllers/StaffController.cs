using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;


namespace WebApplication3.Controllers {
    public class StaffController : Controller {

        private IDbManager dbManager;
        private PositionManager positionManager;
        private EmployeeManager employeeManager;

        public StaffController() {
            dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
            positionManager = new PositionManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(string find = "") {


            if (String.IsNullOrEmpty(find))
                ViewBag.Employees = employeeManager.GetAllEmployee();
            else
                ViewBag.Employees = employeeManager.GetEmployeesByStringFind(find);
            dbManager.Commit();
            return View();
        }


        public IActionResult Initialize() {
            dbManager.Commit();
            new InitializeDb().Start();
            return View("Result", "База успешно инициализрована!");

        }


        [HttpGet]
        public IActionResult Add(int id) {
            ViewBag.Positions = positionManager.GetAll();
            Employee emp;
            if (id != 0)
                emp = employeeManager.GetEmployeeById(id);
            else
                emp = new Employee();

            dbManager.Commit();
            return View("Add", emp);
        }
        [HttpPost]
        public IActionResult Add(Employee emp, int PosId, int id) {
            emp.Position = positionManager.GetById(PosId);
            //employeeManager.Compare(emp);

            bool result = true; ;

            if (id == 0)
                result = employeeManager.CreateNewEmployee(emp);
            else
                result = employeeManager.UpdateEmployee(emp, id);

            string message = result ? "Объект сохранён" : "Не удалось";

            dbManager.Commit();
            return View("Result", message);
        }

        [HttpGet]
        public IActionResult Find() {

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id) {
            bool result = employeeManager.DeleteEmployee(id);
            string message = result ? "Объект удалён" : "Не удалось";

            dbManager.Commit();
            return View("Result", message);
        }

    }

}
