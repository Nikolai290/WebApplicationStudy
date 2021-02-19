using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Entities;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class OrderManager {
        private readonly IDbManager dbManager;
        private readonly EmployeeManager employeeManager;

        public OrderManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            employeeManager = new EmployeeManager(dbManager as DbManager);
        }

        public IList<Machinery> GetAddingListMachinesExcludeRepeats(Order order) {
            var machUnique = dbManager.GetAll<Machinery>().ToList();
            var busyMachs = GetAllBusyMachinesOnThisDateAndShift(order);
            return machUnique.Where(x => !busyMachs.Where(z => x.Id == z.MachineryId).Any()).ToList();
        }

        public List<Order> GetAllOrderOnThisDateAndShift(Order order)
            => GetAll().Where(x => x.Date == order.Date && x.Shift == order.Shift).ToList();

        public List<MachineryOnShift> GetAllBusyMachinesOnThisDateAndShift(Order order) {
            var machs = new List<MachineryOnShift>();
            GetAllOrderOnThisDateAndShift(order).ForEach(x => machs.AddRange(x.Machineries));
            return machs;
        }



        public async Task<OrderIndexViewModel> GetOrderIndexViewModel(OrderGetDTO dto) {
            var model = new OrderIndexViewModel {
                Order = dto.OrderIdForce != 0 ? await dbManager.GetByIdAsync<Order>(dto.OrderIdForce) : Get(dto)
            };
            FillViewModel(model);
            return model;
        }

        private OrderIndexViewModel FillViewModel(OrderIndexViewModel model) {
            model.Areas = dbManager.GetAll<OrderArea>().ToList();
            model.Dispetchers = employeeManager.GetFreeDispetchers(model.Order);
            model.Chiefs = employeeManager.GetFreeChiefs(model.Order);
            model.MiningMasters = employeeManager.GetFreeMasters(model.Order);
            model.Machines = GetAddingListMachinesExcludeRepeats(model.Order);
            return model;
        }

        public async Task<bool> AddNewMachineryOnShift(AddMachintPostDTO dto) {
            bool result;
            var Area = dbManager.GetById<QuarryArea>(dto.AreaId);
            var Field = dbManager.GetById<QuarryField>(dto.FieldId);
            var Horizon = dbManager.GetById<QuarryHorizon>(dto.HorizonId);
            var Group = dbManager.GetById<Group>(dto.GroupId);
            var Plast = dbManager.GetById<QuarryPlast>(dto.PlastId);
            var Crew = dbManager.GetByListId<Employee>(dto.Crew);
            bool PZO = dto.PZO == "on";
            bool HighAsh = dto.HighAsh == "on";
            var order = await dbManager.GetByIdAsync<Order>(dto.OrderId);

            // insert validation here

            MachineryOnShift obj;

            if (dto.MoSId > 0) {
                obj = dbManager.GetById<MachineryOnShift>(dto.MoSId);
                result = true;
            } else {
                var mach = dbManager.GetById<Machinery>(dto.MachineId);
                obj = new MachineryOnShift(mach);
                order.AddMachines(obj);
                obj.SetOrder(order);
                result = dbManager.AddAsync(obj);
            }
            obj.SetLocation(Area, Field, Horizon, Plast, dto.Picket)
                .SetGroup(Group, dto.Number)
                .SetOrderProperties(dto.Weight, dto.Volume, dto.Overex, dto.Ash, dto.Heat, dto.Wet, HighAsh)
                .SetDownTime(dto.Transport, dto.Repair, dto.HoliDays)
                .SetCrew(Crew)
                .SetPZO(PZO);
            return result;
        }

        internal AddingMachineViewModel GetAddingMachineViewModel(AddMachineGetDTO dtoGet) {
            var model = new AddingMachineViewModel {
                OrderId = dtoGet.OrderId,
                MachineryOnShift = dtoGet.MoSId > 0 ?
                dbManager.GetById<MachineryOnShift>(dtoGet.MoSId) :
                new MachineryOnShift(dbManager.GetById<Machinery>(dtoGet.MachineId))
            };
            model = FillViewModelForAddingMachine(model);
            return model;
        }

        private AddingMachineViewModel FillViewModelForAddingMachine(AddingMachineViewModel model) {
            model.Areas = dbManager.GetAll<QuarryArea>().ToList();
            model.Fields = dbManager.GetAll<QuarryField>().ToList();
            model.Horizons = dbManager.GetAll<QuarryHorizon>().ToList();
            model.Groups = dbManager.GetAll<Group>().ToList();
            model.Plasts = dbManager.GetAll<QuarryPlast>().ToList();
            model.FreeDrivers = employeeManager.GetFreeDrivers(model.OrderId, model.MachineryOnShift.Id);
            return model;
        }

        public async Task<OrderIndexViewModel> PostOrderIndexViewModel(OrderGetDTO dtoGet, OrderPostDTO dtoPost) {
            var model = new OrderIndexViewModel {
                Order = dtoPost.OrderId > 0 ?
                await dbManager.GetByIdAsync<Order>(dtoPost.OrderId) :
                Get(dtoGet)
            };
            FillViewModel(model);

            var disp = dtoPost.DispetcherId > 0 ? dbManager.GetById<Employee>(dtoPost.DispetcherId) : model.Order.Dispetcher;
            var chief = dtoPost.ChiefId > 0 ? dbManager.GetById<Employee>(dtoPost.ChiefId) : model.Order.Chief;
            var masters = dtoPost?.MastersId?.Length > 0 && dtoPost.MastersId.Any(x => x > 0) ? dbManager.GetByListId<Employee>(dtoPost?.MastersId) : model.Order?.MiningMaster;
            model.Order
                .SetStaff(disp, chief, masters);

            if (model.Order.Id == 0)
                dbManager.AddAsync(model.Order);

            return model;
        }

        public bool DeleteMachineryOnShift(int id) {
            var obj = dbManager.GetById<MachineryOnShift>(id);
            obj.SetNulls();
            return dbManager.DeleteAsync(obj);
        }

        public IQueryable<Order> GetAll() => dbManager.GetAll<Order>();
        public Order Get(DateTime date, int shiftId, int orderAreaId) {
            // date format: ("yyyy-MM-dd")
            Order result;
            try {
                result = GetAll().Where(x => x.Date == date && x.Shift == shiftId && x.Area.Id == orderAreaId).First();

            } catch {
                result = DefaultOrder(date, shiftId, orderAreaId);
            }


            return result;
        }
        private Order DefaultOrder(DateTime date, int shiftId, int orderAreaId) 
            =>new Order().SetBase(date, shiftId).SetArea(dbManager.GetById<OrderArea>(orderAreaId));
        

        public Order Get(OrderGetDTO dto)
            => Get(dto.Date, dto.ShiftId, dto.OrderAreaId);
    }
}
