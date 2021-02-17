using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class WorkMap : ClassMap<Work> {
        public WorkMap() {
            Id(x => x.Id);
            References(x => x.Parent).Cascade.All();
            References(x => x.Type).Not.LazyLoad();
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.TotalMinutes);
            Map(x => x.StartPosition);
            Map(x => x.Note);

            //
            References(x => x.Sort).Not.LazyLoad();
            Map(x => x.Weight);
            Map(x => x.Ash);
            Map(x => x.Heat);
            Map(x => x.Wet);
            Map(x => x.Wagons);
        }
    }
}
 