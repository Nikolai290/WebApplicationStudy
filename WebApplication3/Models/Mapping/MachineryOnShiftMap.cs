using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class MachineryOnShiftMap : ClassMap<MachineryOnShift> {
        
        public MachineryOnShiftMap() {

            // Base
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.MachineryId);
            References(x => x.Order).Cascade.All();


            // Lacation
            References(x => x.Area).Not.LazyLoad().Cascade.All();
            References(x => x.Field).Not.LazyLoad().Cascade.All();
            References(x => x.Plast).Not.LazyLoad().Cascade.All();
            References(x => x.Horizon).Not.LazyLoad().Cascade.All();
            Map(x => x.Picket);

            // Group
            References(x => x.Group).Not.LazyLoad().Cascade.All();
            Map(x => x.Number);

            // Order
            Map(x => x.Weight);
            Map(x => x.Volume);
            Map(x => x.Overexcavation);
            Map(x => x.Ash);
            Map(x => x.Heat);
            Map(x => x.Wet);
            Map(x => x.HighAsh);

            // Downtime
            Map(x => x.TransportingTime);
            Map(x => x.RepairingTime);
            Map(x => x.HolidayTime);

            // Crew
            HasManyToMany(x => x.Crew).Not.LazyLoad().Cascade.All();

            // PZO
            Map(x => x.PZO);

        }
    }
}
