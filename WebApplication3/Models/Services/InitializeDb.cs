using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class InitializeDb {

        private readonly DbManager dbManager;
        private readonly EmployeeManager employeeManager;

        public InitializeDb() {
            dbManager = new DbManager();
            employeeManager = new EmployeeManager(dbManager);
        }

        public void Start() {
            DbAccess.DbAccess.Rebuild();
            dbManager.GetAll<OrderArea>();
            Initialize();
            dbManager.Commit();
        }

        private void Initialize() {
            var orderAreas = new List<OrderArea> {
                new OrderArea("Добыча"),
                new OrderArea("Вскрыша"),
                new OrderArea("Автовскрыша")
            };

            var quarryAreas = new List<QuarryArea> {
                new QuarryArea("Восток"),
                new QuarryArea("Запад"),
                new QuarryArea("Прочие"),
                new QuarryArea("Ремонт"),
                new QuarryArea("Добыча")
            };

            var QuarryFields = new List<QuarryField> {
                new QuarryField("Добыча"),
                new QuarryField("Вскрыша"),
                new QuarryField("Отвал"),
                new QuarryField("Прочие")
            };

            var QuarryHorizons = new List<QuarryHorizon> {
                new QuarryHorizon("1 горизонт"),
                new QuarryHorizon("2 горизонт"),
                new QuarryHorizon("3 горизонт"),
                new QuarryHorizon("Междупластье")
            };

            var QuarryPlasts = new List<QuarryPlast> {
                new QuarryPlast("Бородинский-1"),
                new QuarryPlast("Бородинский-2"),
                new QuarryPlast("Рыбинский-1"),
                new QuarryPlast("Рыбинский-2")
            };

            var Groups = new List<Group> {
                new Group(1),
                new Group(2),
                new Group(3),
                new Group(4),
                new Group(5),
                new Group(6),
                new Group(16),
                new Group(34)
            };

            var Machineries = new List<Machinery> {
                new Machinery("Д-155 №3"),
                new Machinery("Д-155 №4"),
                new Machinery("Д-155 №5"),
                new Machinery("Д65-12 №11"),
                new Machinery("ЖД весовая"),
                new Machinery("МСГ-2000"),
                new Machinery("МСУ"),
                new Machinery("Насосная"),
                new Machinery("ПДСУ"),
                new Machinery("РС-300-7 №14"),
                new Machinery("РС-300-7 №16"),
                new Machinery("ЭРП-2500 №3"),
                new Machinery("ЭРП-2500 №4"),
                new Machinery("ЭРП-1250 №4"),
                new Machinery("ЭРП-1250 №5"),
                new Machinery("ЭРП-1250 №6"),
                new Machinery("DOOSAN Solar №15"),
                new Machinery("HITACHI ZX850 №17"),
                new Machinery("KOMATSU HD 785-7 №26"),
                new Machinery("KOMATSU HD 785-7 №27"),
                new Machinery("KOMATSU HD 785-7 №4"),
                new Machinery("KOMATSU HD 785-7 №5"),
                new Machinery("KOMATSU HD 785-7 №54")
            };

            var CoalSorts = new List<CoalSort> {
                new CoalSort("2 БР"),
                new CoalSort("2 БПКО"),
                new CoalSort("Сорт технология"),
                new CoalSort("Сорт МСГ-2000"),
                new CoalSort("Сорт МСУ"),
                new CoalSort("2 БР (отсев)"),
                new CoalSort("2Б (технология)"),
                new CoalSort("2Б (МСГ-2000)"),
                new CoalSort("2Б (МСУ)")
            };

            var figures = new List<Figure> {
                new Figure("rectangle"),
                new Figure("trapeze")
            };

            var TypeWorks = new List<WorkTypes> {
                new WorkTypes("Технологические операции", "orange", "black", figures[0]),
                new WorkTypes("Дробление горельника", "green", "white", figures[0]),
                new WorkTypes("Погрузка угля в автотранспорт", "rebeccapurple", "white", figures[1]),
                new WorkTypes("Обогащение угля", "royalblue", "white", figures[0])
            };

            var Positions = new List<Position> {
                new Position("Нет должности"),
                new Position("Главный диспетчер"),
                new Position("Начальник"),
                new Position("Горный мастер"),
                new Position("Машинист", "Машинист экскаватора 6 разряда"),
                new Position("Машинист", "Машинист экскаватора 5 разряда"),
                new Position("Машинист", "Машинист экскаватора 7 разряда"),
                new Position("Машинист", "Машинист бульдозера 7 разряда"),
                new Position("Машинист", "Водитель автомобиля"),
                new Position("Машинист", "Машинист насосных установок"),
                new Position("Машинист", "Помошник машинста экскаватора 4 разряда"),
                new Position("Машинист", "Выгрузчик на отвалах")
            };
            PullToDb(Positions);
            int pos = Positions.Count;

            var names = new List<string> {
                "Василий",
                "Иннокентий",
                "Афанасий",
                "Петр",
                "Алексей",
                "Михаил",
                "Сергей",
                "Даниил",
                "Максим",
                "Артём",
                "Андрей",
                "Павел",
                "Геннадий",
                "Иван",
                "Фёдор",
                "Альфред",
                "Дмитрий",
                "Никита",
                "Владимир",
                "Вячеслав",
                "Владислав",
                "Валентин",
                "Олег",
                "Илья",
                "Роберт"
            };

            var lastnames = new List<string> {
                "Петров",
                "Афанасьев",
                "Иннокеньтев",
                "Сергеев",
                "Данилов",
                "Алексеев",
                "Алексеев",
                "Иванов",
                "Николаев",
                "Павлов",
                "Максимов",
                "Малахов",
                "Макаревич",
                "Александров",
                "Собакин",
                "Кошкин",
                "Синицин",
                "Пупкин",
                "Усик"
            };

            var fathernames = new List<string> {
                "Геннадьевич",
                "Михайлович",
                "Петрович",
                "Васильевич",
                "Сергеевич",
                "Алексеевич",
                "Петрович",
                "Афанасьевич",
                "Андреевич",
                "Артёмович",
                "Максимович",
                "Вадимович",
                "Николаевич",
                "Павлович",
                "Фёдорович",
                "Владимирович",
                "Вячеславович",
                "Валентинович",
                "Владиславович",
                "Олегович",
                "Ильич"
            };

            var Employees = new List<Employee>();

            while (Employees.Count < 100)
                Employees.Add(new Employee(names[Rnd(names.Count)], lastnames[Rnd(lastnames.Count)], fathernames[Rnd(fathernames.Count)], Rnd(), Positions[Rnd(pos)]));

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
            PullToDb(figures);
            PullToDb(CoalSorts);
            PullToDb(TypeWorks);


            var disps = Employees.Where(x => x.Position.Id == 2);
            var chiefs = Employees.Where(x => x.Position.Id == 3);
            var masters = Employees.Where(x => x.Position.Id == 4).ToList();


            while (masters.Count > 2)
                masters.RemoveAt(Rnd(masters.Count));
/*
            Order order = new Order();
            order.SetBase(DateTime.Now.Date, 1).SetStaff(disps.First(), chiefs.First(), masters).SetArea(orderAreas.First());

            dbManager.AddAsync(order);*/
        }

        private static int Rnd(int max) => Rnd(0, max);

        private static int Rnd(int min = 1000, int max = 1000000) {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public void PullToDb<T>(IList<T> objs) where T : DbEntities {
            foreach (var obj in objs) {
                dbManager.AddAsync(obj);
            }
        }

    }
}
