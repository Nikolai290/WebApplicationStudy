using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Services {
    public class WorkManager {
        IDbManager dbManager;

        public WorkManager(IDbManager dbManager) {
            this.dbManager = dbManager;
        }

        public bool Create(int machId) {



            return true;
        }

        public List<WorkTypes> GetTypes()
            => dbManager.GetAll<WorkTypes>().ToList();
        public WorkTypes GetTypeById(int id)
            => GetTypes().Where(x => x.Id == id).First();

        public List<CoalSort> GetSorts()
            => dbManager.GetAll<CoalSort>().ToList();

        public CoalSort GetSortById(int id)
            =>GetSorts().Where(x => x.Id == id).First();

        public void Delete(int workId) {
            var work = GetById(workId);
            var mach = dbManager.GetById<MachineryOnShift>(work.Parent.Id);

            mach.Works.Remove(work);
            work.SetParent(null);

            dbManager.Delete(work);
        }

        public Work GetById(int id) => dbManager.GetById<Work>(id);

        internal Work NewWork(
            int workId, int machId, int typework, DateTime startTime, DateTime endTime, 
            string note, 
            int sort, double volume, double weight, double ash, double heat, double wet, 
            int wagons) {

            var mach = dbManager.GetById<MachineryOnShift>(machId);

            var work = workId > 0? GetById(workId) : new Work().SetParent(mach);
            var order = dbManager.GetById<Order>(mach.Order.Id);

            bool isRightTime = CheckTime(order, startTime, endTime);
            if (!isRightTime)
                return new Work();

            work.SetType(GetTypeById(typework))
                .SetNote(note)
                .SetTime(startTime, endTime);

            if (work.Type.Id == 2) {
                work.SetVolume(volume);
            } else if(work.Type.Id == 3 || work.Type.Id == 4) {
                work.SetProperties(weight, ash, heat, wet, GetSortById(sort));
                if (work.Type.Id == 4) {
                    work.SetWagons(wagons);
                }
            }


            dbManager.Add(work);

            return work;
        }

        private bool CheckTime(Order order, DateTime startTime, DateTime endTime) {
            if (startTime > endTime)
                return false;

            bool result = false;

            DateTime minTime;
            DateTime maxTime;

            if (order.Shift == 1) {
                minTime = order.Date.AddHours(8);
            } else {
                minTime = order.Date.AddHours(20);
            }
            maxTime = minTime.AddHours(11).AddMinutes(59);

            if (IsValidTime(startTime, endTime, minTime, maxTime)) {
                result = true;
            }

            return result;
        }

        private bool IsValidTime(DateTime startTime, DateTime endTime, DateTime minTime, DateTime maxTime) {
            bool result = true;
            if (startTime < minTime && startTime > maxTime)
                return false;
            if (endTime < minTime && startTime > maxTime)
                return false;
            return result;
        }
    }
}
