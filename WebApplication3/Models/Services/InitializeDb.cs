using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class InitializeDb {

        private IDbManager dbManager;
        private EmployeeManager employeeManager;
        private OrderManager orderManager;

        public InitializeDb() {
            this.dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
            orderManager = new OrderManager(dbManager);
        }

        public void Start() {
            DbAccess.DbAccess.Rebuild();
            Initialize();
            dbManager.Commit();
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
            int pos = Positions.Count;

            var poss = dbManager.GetAll<Position>();
            var names = new List<string>();
            names.Add("Василий");
            names.Add("Иннокентий");
            names.Add("Афанасий");
            names.Add("Петр");
            names.Add("Алексей");
            names.Add("Михаил");
            names.Add("Сергей");
            names.Add("Даниил");
            names.Add("Максим");
            names.Add("Артём");
            names.Add("Андрей");
            names.Add("Павел");
            names.Add("Геннадий");
            names.Add("Иван");
            names.Add("Фёдор");
            names.Add("Альфред");
            names.Add("Дмитрий");
            names.Add("Никита");
            names.Add("Владимир");
            names.Add("Вячеслав");
            names.Add("Владислав");
            names.Add("Валентин");
            names.Add("Олег");
            names.Add("Илья");
            names.Add("Роберт");

            var lastnames = new List<string>();
            lastnames.Add("Петров");
            lastnames.Add("Афанасьев");
            lastnames.Add("Иннокеньтев");
            lastnames.Add("Сергеев");
            lastnames.Add("Данилов");
            lastnames.Add("Алексеев");
            lastnames.Add("Алексеев");
            lastnames.Add("Иванов");
            lastnames.Add("Николаев");
            lastnames.Add("Павлов");
            lastnames.Add("Максимов");
            lastnames.Add("Малахов");
            lastnames.Add("Макаревич");
            lastnames.Add("Александров");
            lastnames.Add("Собакин");
            lastnames.Add("Кошкин");
            lastnames.Add("Синицин");
            lastnames.Add("Пупкин");
            lastnames.Add("Усик");

            var fathernames = new List<string>();
            fathernames.Add("Геннадьевич");
            fathernames.Add("Михайлович");
            fathernames.Add("Петрович");
            fathernames.Add("Васильевич");
            fathernames.Add("Сергеевич");
            fathernames.Add("Алексеевич");
            fathernames.Add("Петрович");
            fathernames.Add("Афанасьевич");
            fathernames.Add("Андреевич");
            fathernames.Add("Артёмович");
            fathernames.Add("Максимович");
            fathernames.Add("Вадимович");
            fathernames.Add("Николаевич");
            fathernames.Add("Павлович");
            fathernames.Add("Фёдорович");
            fathernames.Add("Владимирович");
            fathernames.Add("Вячеславович");
            fathernames.Add("Валентинович");
            fathernames.Add("Владиславович");
            fathernames.Add("Олегович");
            fathernames.Add("Ильич");

            IList<Employee> Employees = new List<Employee>();

            while (Employees.Count < 50)
                Employees.Add(new Employee(names[Rnd(names.Count)], lastnames[Rnd(lastnames.Count)], fathernames[Rnd(fathernames.Count)], Rnd(), poss[Rnd(pos)]));

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

            while (masters.Count > 2)
                masters.RemoveAt(Rnd(masters.Count));

            Order order = new Order();
            order.SetBase(DateTime.Now.Date, 1).SetStaff(disps.First(), chiefs.First(), masters).SetArea(orderArea);

            orderManager.Create(order);
        }

        private int Rnd(int max) => Rnd(0, max);

        private int Rnd(int min = 1000, int max = 1000000) {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public void PullToDb<T>(IList<T> objs) where T : DbEntities {
            foreach (var obj in objs) {
                dbManager.Add(obj);
            }
        }

    }
}
