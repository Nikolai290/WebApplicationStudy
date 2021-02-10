using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.Services;

namespace WebApplication3.Controllers {
    public class OrderController : Controller {


        private IDbManager dbManager;
        private OrderManager orderManager;
        private EmployeeManager employeeManager;
        private OrderAreaManager areaManager;
        private MachineryOnShiftManager machOnShiftManager;

        public OrderController() {
            dbManager = new DbManager();
            orderManager = new OrderManager(dbManager);
            employeeManager = new EmployeeManager(dbManager);
            areaManager = new OrderAreaManager(dbManager);
            machOnShiftManager = new MachineryOnShiftManager(dbManager);
        }

        [HttpGet]
        public IActionResult Index(DateTime Date, int Shift = 1, int OrderAreaId = 1) {
            if (Date.Year < 2000)
                Date = DateTime.Now.Date;
            Order order = orderManager.Get(Date, Shift, OrderAreaId);

            ViewBag.Areas = areaManager.GetAll();
            ViewBag.Disps = employeeManager.GetEmployeesByStringFind("Диспетчер");
            ViewBag.Chiefs = employeeManager.GetEmployeesByStringFind("Начальник");
            ViewBag.Masters = employeeManager.GetEmployeesByStringFind("Горный мастер");
            ViewBag.Machines = orderManager.GetAddingListMachinesExcludeRepeats(order);
            ViewBag.MachOnShift = order?.Machineries;
            dbManager.Commit();
            return View("Order", order);
        }

        [HttpPost]
        public IActionResult Index(DateTime date, int shift, int orderAreaId, int Dispetcher, int Chief, int[] Masters) {
            var areas = areaManager.GetAll();

            var disp = employeeManager.GetEmployeeById(Dispetcher);
            var chief = employeeManager.GetEmployeeById(Chief);
            var masters = new List<Employee>();
            foreach (var id in Masters)
                masters.Add(employeeManager.GetEmployeeById(id));

            Order order = new Order()
                .SetBase(date, shift)
                .SetStaff(disp, chief, masters)
                .SetArea(areas.Where(x => x.Id == orderAreaId).First());

            orderManager.SaveOrUpdate(order);
            ViewBag.Areas = areas;
            ViewBag.Disps = employeeManager.GetEmployeesByStringFind("Диспетчер");
            ViewBag.Chiefs = employeeManager.GetEmployeesByStringFind("Начальник");
            ViewBag.Masters = employeeManager.GetEmployeesByStringFind("Горный мастер");

            dbManager.Commit();
            return View("Order", order);
        }

        [HttpGet]
        public IActionResult AddMachineToOrder(int Order, string Machine) {

            ViewBag.OrderId = Order;
            ViewBag.Machine = Machine;
            ViewBag.Areas = machOnShiftManager.GetAreas();
            ViewBag.Fields = machOnShiftManager.GetFields();
            ViewBag.Horizons = machOnShiftManager.GetHorizons();
            ViewBag.Groups = machOnShiftManager.GetGroups();
            ViewBag.Plasts = machOnShiftManager.GetPlasts();
            ViewBag.Crew = employeeManager.GetEmployeesByStringFind("Машинист");

            dbManager.Commit();
            return View("AddMachineToOrder");
        }


        [HttpPost]
        public IActionResult AddMachineToOrder(
            int OrderId,
            string machine,
            int area,
            int field,
            int plast,
            int horizon,
            int group,
            int[] crew,
            bool highash,
            bool pzo,
            int number=0,
            double picket = 0,
            double weight = 0,
            double volume = 0,
            double overex = 0,
            double ash = 0,
            double heat = 0,
            double wet = 0,
            int transport=0,
            int repair=0,
            int holidays=0,
            int machId = 0
            ) {

            var Area = machOnShiftManager.GetAreas().Where(x => x.Id == area).First();
            var Field = machOnShiftManager.GetFields().Where(x => x.Id == field).First();
            var Horizon = machOnShiftManager.GetHorizons().Where(x => x.Id == horizon).First();
            var Group = machOnShiftManager.GetGroups().Where(x => x.Id == group).First();
            var Plast = machOnShiftManager.GetPlasts().Where(x => x.Id == plast).First();

            var empls = employeeManager.GetAllEmployee();
            IList<Employee> Crew = new List<Employee>();
            foreach(var id in crew) {
                Crew.Add(empls.Where(x => x.Id == id).First());
            }

            var order = orderManager.GetById(OrderId);
            MachineryOnShift Machine;

            if (machId > 0)
                Machine = machOnShiftManager.GetById(machId);
            else {
                Machine = new MachineryOnShift(machine);
                order.AddMachines(Machine);
                Machine.SetOrder(order);
            }
            Machine.SetLocation(Area, Field, Horizon, Plast, picket)
                .SetGroup(Group, number)
                .SetOrderProperties(weight, volume, overex, ash, heat, wet, highash)
                .SetDownTime(transport, repair, holidays)
                .SetCrew(Crew)
                .SetPZO(pzo);

            machOnShiftManager.Create(Machine);
            dbManager.Commit();
            return View("Result", machine);
        }

    }
}
