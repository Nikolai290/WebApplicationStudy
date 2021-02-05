using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class InitializeDb {

        DbManager dbManager = new DbManager();
        EmployeeManager employeeManager = new EmployeeManager();
        OrderManager orderManager = new OrderManager();

        public void Start() {
            DbAccess.DbAccess.Rebuild();
            Initialize();

        }

        private void Initialize() {
            IList<OrderArea> orderAreas = new List<OrderArea>();
            orderAreas.Add(new OrderArea("Добыча"));
            orderAreas.Add(new OrderArea("Вскрыша"));
            orderAreas.Add(new OrderArea("Автовскрыша"));

            IList<QuarryArea> quarryAreas = new List<QuarryArea>();
            quarryAreas.Add(new QuarryArea("Восток"));
            quarryAreas.Add(new QuarryArea("Запад"));
            quarryAreas.Add(new QuarryArea("Прочие"));
            quarryAreas.Add(new QuarryArea("Ремонт"));
            quarryAreas.Add(new QuarryArea("Добыча"));

            IList<QuarryField> QuarryFields = new List<QuarryField>();
            QuarryFields.Add(new QuarryField("Добыча"));
            QuarryFields.Add(new QuarryField("Вскрыша"));
            QuarryFields.Add(new QuarryField("Отвал"));
            QuarryFields.Add(new QuarryField("Прочие"));

            IList<QuarryHorizon> QuarryHorizons = new List<QuarryHorizon>();
            QuarryHorizons.Add(new QuarryHorizon("1 горизонт"));
            QuarryHorizons.Add(new QuarryHorizon("2 горизонт"));
            QuarryHorizons.Add(new QuarryHorizon("3 горизонт"));
            QuarryHorizons.Add(new QuarryHorizon("Междупластье"));

            IList<QuarryPlast> QuarryPlasts = new List<QuarryPlast>();
            QuarryPlasts.Add(new QuarryPlast("Бородинский-1"));
            QuarryPlasts.Add(new QuarryPlast("Бородинский-2"));
            QuarryPlasts.Add(new QuarryPlast("Рыбинский-1"));
            QuarryPlasts.Add(new QuarryPlast("Рыбинский-2"));


            IList<Group> Groups = new List<Group>();
            Groups.Add(new Group(1));
            Groups.Add(new Group(2));
            Groups.Add(new Group(3));
            Groups.Add(new Group(4));
            Groups.Add(new Group(5));
            Groups.Add(new Group(6));
            Groups.Add(new Group(16));
            Groups.Add(new Group(34));

            IList<Machinery> Machineries = new List<Machinery>();
            Machineries.Add(new Machinery("Д-155 №3"));
            Machineries.Add(new Machinery("Д-155 №4"));
            Machineries.Add(new Machinery("Д-155 №5"));
            Machineries.Add(new Machinery("Д65-12 №11"));
            Machineries.Add(new Machinery("ЖД весовая"));
            Machineries.Add(new Machinery("МСГ-2000"));
            Machineries.Add(new Machinery("МСУ"));
            Machineries.Add(new Machinery("Насосная"));
            Machineries.Add(new Machinery("ПДСУ"));
            Machineries.Add(new Machinery("РС-300-7 №14"));
            Machineries.Add(new Machinery("РС-300-7 №16"));
            Machineries.Add(new Machinery("ЭРП-2500 №3"));
            Machineries.Add(new Machinery("ЭРП-2500 №4"));
            Machineries.Add(new Machinery("ЭРП-1250 №4"));
            Machineries.Add(new Machinery("ЭРП-1250 №5"));
            Machineries.Add(new Machinery("ЭРП-1250 №6"));
            Machineries.Add(new Machinery("DOOSAN Solar №15"));
            Machineries.Add(new Machinery("HITACHI ZX850 №17"));
            Machineries.Add(new Machinery("KOMATSU HD 785-7 №26"));
            Machineries.Add(new Machinery("KOMATSU HD 785-7 №27"));
            Machineries.Add(new Machinery("KOMATSU HD 785-7 №4"));
            Machineries.Add(new Machinery("KOMATSU HD 785-7 №5"));
            Machineries.Add(new Machinery("KOMATSU HD 785-7 №54"));




            IList<Position> Positions = new List<Position>();
            Positions.Add(new Position("Нет должности"));
            Positions.Add(new Position("Главный диспетчер"));
            Positions.Add(new Position("Начальник"));
            Positions.Add(new Position("Горный мастер"));
            Positions.Add(new Position("Машинист", "Машинист экскаватора 6 разряда"));
            Positions.Add(new Position("Машинист", "Машинист экскаватора 5 разряда"));
            Positions.Add(new Position("Машинист", "Машинист экскаватора 7 разряда"));
            Positions.Add(new Position("Машинист", "Машинист бульдозера 7 разряда"));
            Positions.Add(new Position("Машинист", "Водитель автомобиля"));
            Positions.Add(new Position("Машинист", "Машинист насосных установок"));
            Positions.Add(new Position("Машинист", "Помошник машинста экскаватора 4 разряда"));
            Positions.Add(new Position("Машинист", "Выгрузчик на отвалах"));
            PullToDb(Positions);

            var poss = dbManager.GetAll<Position>();
            IList<Employee> Employees = new List<Employee>();
            Employees.Add(new Employee("Василий", "Петров", "Геннадьевич", 1234, poss[1]));
            Employees.Add(new Employee("Иннокентий", "Афанасьев", "Михайлович", 34532, poss[1]));
            Employees.Add(new Employee("Афанасий", "Иннокеньтев", "Петрович", 45345, poss[1]));
            Employees.Add(new Employee("Петр", "Сергеев", "Васильевич", 4523, poss[2]));
            Employees.Add(new Employee("Алексей", "Иванов", "Сергеевич", 3454, poss[2]));
            Employees.Add(new Employee("Михаил", "Данилов", "Алексеевич", 6345, poss[2]));
            Employees.Add(new Employee("Сергей", "Алексеев", "Петрович", 26576, poss[3]));
            Employees.Add(new Employee("Даниил", "Петров", "Андреевич", 34235, poss[3]));
            Employees.Add(new Employee("Сергей", "Алексеев", "Петрович", 24923, poss[3]));
            Employees.Add(new Employee("Алексей", "Васильев", "Сергеевич", 725246, poss[4]));
            Employees.Add(new Employee("Иннокентий", "Иванов", "Афанасьевич", 3634, poss[4]));
            Employees.Add(new Employee("Алексей", "Васильев", "Сергеевич", 2342, poss[4]));
            Employees.Add(new Employee("Максим", "Николаев", "Андреевич", 635418, poss[5]));
            Employees.Add(new Employee("Федор", "Максимов", "Артёмович", 5235, poss[5]));
            Employees.Add(new Employee("Артём", "Малахов", "Максимович", 7567, poss[5]));
            Employees.Add(new Employee("Андрей", "Макаревич", "", 78861, poss[6]));
            Employees.Add(new Employee("Николай", "Максимов", "Николаевич", 87654, poss[7]));
            Employees.Add(new Employee("Геннадий", "Малахов", "Андреевич", 45368, poss[8]));
            Employees.Add(new Employee("Алексей", "Иванов", "Васильевич", 72378, poss[8]));
            Employees.Add(new Employee("Андрей", "Николаев", "Геннадьевич", 56721, poss[9]));
            Employees.Add(new Employee("Николай", "Александров", "Михайлович", 6453, poss[9]));
            Employees.Add(new Employee("Федор", "Иванов", "Михайлович", 56782, poss[10]));
            Employees.Add(new Employee("Иван", "Петров", "Сергеевич", 56894, poss[10]));
            Employees.Add(new Employee("Александр", "Иванов", "Васильевич", 42348, poss[11]));
            Employees.Add(new Employee("Андрей", "Собакин", "Александрович", 1523, poss[6]));
            //PullToDb(Employees);
            foreach (var emp in Employees) {
                employeeManager.CreateNewEmployee(emp);
            }






            PullToDb(orderAreas);
            PullToDb(quarryAreas);
            PullToDb(QuarryFields);
            PullToDb(QuarryHorizons);
            PullToDb(QuarryPlasts);
            PullToDb(Groups);
            PullToDb(Machineries);

            var disps = employeeManager.GetEmployeesByStringFind("диспетчер");
            var chiefs = employeeManager.GetEmployeesByStringFind("начальник");
            var masters = employeeManager.GetEmployeesByStringFind("горный мастер");
            var orderArea = dbManager.GetAll<OrderArea>().First();


            Order order = new Order();
            order.SetBase(DateTime.Now.Date, 1).SetStaff(disps.First(), chiefs.First(), masters).SetArea(orderArea);

            orderManager.Create(order);

        }

        public void PullToDb<T>(IList<T> objs) where T : DbEntities {
            foreach (var obj in objs) {
                dbManager.Add(obj);
            }
        }

    }
}
