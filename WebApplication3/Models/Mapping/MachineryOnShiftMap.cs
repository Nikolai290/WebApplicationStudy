using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class MachineryOnShiftMap : ClassMap<MachineryOnShift> {
        
        public MachineryOnShiftMap() {

            // Base
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Order);

            // Lacation
            References(x => x.Area);
            References(x => x.Field);
            References(x => x.Plast);
            References(x => x.Horizon);
            Map(x => x.Picket);

            // Group
            References(x => x.Group);
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
            Map(x => x.ReapairingTime);
            Map(x => x.HolidayTime);

            // Crew
            HasManyToMany(x => x.Crew);

            // PZO
            Map(x => x.PZO);

        }
    }
}
