using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;
using System.Threading.Tasks;

namespace WebApplication3.Models.Services {
    public class EmployeeManager {

        private readonly IDbManager dbManager;

        public EmployeeManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool CreateNewEmployee(Employee emp)
            => dbManager.AddAsync(emp);


        public Employee AddAsync(AddEmployeeViewModel dto) {
            Employee emp;
            dto.Position = dbManager.GetById<Position>(dto.PositionId);

            if (dto.Id > 0) {
                emp = dbManager.GetById<Employee>(dto.Id);
                if (IsNotTrueTableNumber(dto.TableNumber) <= 1) {
                    emp.Create(dto);
                }

            } else {
                emp = new Employee();
                if (IsNotTrueTableNumber(dto.TableNumber) == 0) {
                    emp.Create(dto);
                    dbManager.AddAsync(emp);
                }
            }
            return emp;
        }

        internal AddEmployeeViewModel GetAddEmployeeViewModel(AddEmployeeDTO dto) {
            var model = new AddEmployeeViewModel();
            model.Positions = dbManager.GetAll<Position>().ToList();

            var emp = dto.Id > 0 ?
                 dbManager.GetById<Employee>(dto.Id) :
                 new Employee().SetNameByFind(dto.Find);
            model.CopyFrom(emp);

            return model;
        }

        public bool PseudoDelete(int id)
            => GetById(id).Delete(true);

        public IQueryable<Employee> GetAll()
            => dbManager.GetAll<Employee>();
        public List<Order> GetAllOrderOnThisDateAndShift(Order order)
            => dbManager.GetAll<Order>().Where(x => x.Date == order.Date && x.Shift == order.Shift).ToList();

        public IList<Employee> GetFreeDispetchers(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Dispetcher));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Диспетчер").Difference(busyEmpls);

            if (order.Dispetcher.IsDelete) freeEmpls.Add(order.Dispetcher);

            return freeEmpls;
        }

        public IList<Employee> GetFreeChiefs(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.Add(x.Chief));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Начальник").Difference(busyEmpls);
            if (order.Chief.IsDelete) freeEmpls.Add(order.Chief);

            return freeEmpls;
        }
        public IList<Employee> GetFreeMasters(Order order) {
            var orders = GetAllOrderOnThisDateAndShift(order).Where(x => x.Id != order.Id).ToList();
            var busyEmpls = new List<Employee>();
            orders.ForEach(x => busyEmpls.AddRange(x.MiningMaster));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Горный мастер").Difference(busyEmpls);
            if (order.MiningMaster.Any(x => x.IsDelete))
                freeEmpls.ToList().AddRange(order.MiningMaster.Where(x => x.IsDelete));

            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(Order order, int machId) {
            var machs = GetAllBusyMachinesOnThisDateAndShift(order).Where(x => x.Id != machId).ToList();

            var busyEmpls = new List<Employee>();
            machs?.ForEach(x => busyEmpls.AddRange(x.Crew));
            if (busyEmpls.Count == 0)
                busyEmpls.Add(new Employee());
            var freeEmpls = GetEmployeesByStringFind("Машинист").Difference(busyEmpls);

            if (machId > 0) {
                var aurMach = dbManager.GetById<MachineryOnShift>(machId);
                var isdeletedempls = aurMach.Crew.Where(x => x.IsDelete).ToList();
                isdeletedempls.ForEach(x => freeEmpls.Insert(0, x));
            }


            return freeEmpls;
        }
        public IList<Employee> GetFreeDrivers(int orderId, int machId)
            => GetFreeDrivers(dbManager.GetById<Order>(orderId), machId);

        public List<MachineryOnShift> GetAllBusyMachinesOnThisDateAndShift(Order order) {
            var machs = new List<MachineryOnShift>();
            GetAllOrderOnThisDateAndShift(order).ForEach(x => x.Machineries
            .Where(x => !x.IsDelete).ToList()
            .ForEach(x => machs.Add(x)));
            return machs;
        }



        public Employee GetById(int id)
            => dbManager.GetById<Employee>(id);

        public IList<Employee> GetEmployeeByPosition(Position position)
            => dbManager.GetAll<Employee>().Where(x => x.Position.Id == position.Id).ToList();

        public IList<Employee> GetEmployeesByStringFind(string find)
            => GetAll().ToList().Where(x => x.ToString().ToUpper().Contains(find.ToUpper())).ToList();

        private int IsNotTrueTableNumber(int tableNumber) {
            if (tableNumber > 1000 && tableNumber < 1000000)
                return GetAll().Where(x => x.TableNumber == tableNumber).Count();
            else
                return 999;
        }


    }
}
