﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.DbAccess;
using WebApplication3.Models.Entities;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Services {
    public class TimeLineManager {
        private readonly IDbManager dbManager;
        private readonly OrderManager orderManager;

        public TimeLineManager(IDbManager dbManager) {
            this.dbManager = dbManager;
            orderManager = new OrderManager(dbManager);
        }

        public TimeLineViewModel GetModelForTimeline(TimelineGetDTO dto) {
            var model = new TimeLineViewModel();

            if (dto.Date.Year < 1950)
                dto.Date = DateTime.Now.Date;

            model.Order = dto.OrderId > 0 ?
                dbManager.GetById<Order>(dto.OrderId) :
                orderManager.Get(dto.Date, dto.ShiftId, dto.OrderAreaId);
            model.Work = dto.WorkId > 0 ?
                dbManager.GetById<Work>(dto.WorkId) :
                new Work().SetTime(dto.StartTime, dto.StartTime.AddMinutes(10), model.Order.Shift);
            model.MachineName = dto.MoSId > 0 ? dbManager.GetById<MachineryOnShift>(dto.MoSId).Name : null;

            model.WorkTypes = GetTypes();
            model.CoalSorts = GetSorts();
            model.Areas = dbManager.GetAll<OrderArea>().ToList().Where(x => !String.IsNullOrEmpty(x.Name)).ToList();

            var startHour = model.Order.Shift == 1 ?
                new DateTime(1, 1, 1, 8, 0, 0) :
                new DateTime(1, 1, 1, 20, 0, 0);
            var hours = new List<string>();
            for (int i = 0; i < 12; i++) {
                hours.Add(startHour.AddHours(i).ToString("HH:mm"));
            }
            hours.Add(startHour.AddHours(11).ToString("HH:59"));
            model.Hours = hours;

            var timeIntervals = new List<string>();
            for (int i = 0; i < 72; i++) {
                timeIntervals.Add(startHour.AddMinutes(i * 10).ToString("HH:mm"));
            }

            model.TimeIntervals = timeIntervals;

            return model;
        }

        public void Delete(int workId)
            => dbManager.PseudoDelete<Work>(workId);

        public IList<WorkTypes> GetTypes()
            => dbManager.GetAll<WorkTypes>().ToList();
        public WorkTypes GetTypeById(int id)
            => dbManager.GetById<WorkTypes>(id);
        public IList<CoalSort> GetSorts()
            => dbManager.GetAll<CoalSort>().ToList();

        public CoalSort GetSortById(int id)
            => dbManager.GetById<CoalSort>(id);

        public Work GetById(int id)
            => dbManager.GetById<Work>(id);

        public Work NewWork(AddWorkDTO dto) {
            dto.ConvertStringToDouble();
            return NewWork(dto.WorkId, dto.MoSId, dto.TypeWorkId, dto.StartTime, dto.EndTime,
                dto.Note,
                dto.SortId, dto.Volume, dto.Weight, dto.Ash, dto.Heat, dto.Wet,
                dto.Wagons);
        }


        public Work NewWork(
            int workId, int moSId, int typework, DateTime startTime, DateTime endTime,
            string note,
            int sort, double volume, double weight, double ash, double heat, double wet,
            int wagons) {

            var mach = dbManager.GetById<MachineryOnShift>(moSId);
            var work = workId > 0 ? GetById(workId) : new Work().SetParent(mach);
            var order = dbManager.GetById<Order>(mach.Order.Id);

            bool isRightTime = CheckTime(order, startTime, endTime);
            if (!isRightTime)
                return new Work().SetNote("Укажите правильное время");

            work.SetType(GetTypeById(typework))
                .SetNote(note)
                .SetTime(startTime, endTime, order.Shift);



            if (work.Type.Id == 2) {
                work.SetVolume(volume);
            } else if (work.Type.Id == 3 || work.Type.Id == 4) {
                work.SetProperties(weight, ash, heat, wet, GetSortById(sort));
                if (work.Type.Id == 4) {
                    work.SetWagons(wagons);
                }
            }


            dbManager.AddAsync(work);

            return work;
        }

        private static bool CheckTime(Order order, DateTime startTime, DateTime endTime) {
            if (order.Shift == 2 && startTime<endTime) {
                startTime = startTime.AddHours(-12);
                endTime = endTime.AddHours(-12);
            } else if( order.Shift ==2 && startTime > endTime) {
                startTime = startTime.AddHours(-12);
                endTime = endTime.AddHours(12);
            }

            if (startTime > endTime)
                return false;

            bool result = false;

            DateTime minTime;
            DateTime maxTime;


            minTime = order.Date.AddHours(8);

            maxTime = minTime.AddHours(11).AddMinutes(59);

            if (IsValidTime(startTime, endTime, minTime, maxTime)) {
                result = true;
            }

            if (order.Shift == 2 && startTime < endTime) {
                startTime = startTime.AddHours(12);
                endTime = endTime.AddHours(12);
            } else if (order.Shift == 2 && startTime > endTime) {
                startTime = startTime.AddHours(12);
                endTime = endTime.AddHours(-12);

            }
            return result;
        }

        private static bool IsValidTime(DateTime startTime, DateTime endTime, DateTime minTime, DateTime maxTime) {
            if (startTime < minTime && startTime > maxTime)
                return false;
            if (endTime < minTime && startTime > maxTime)
                return false;
            return true;
        }
    }
}
