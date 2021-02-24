using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class WorkTypesMap : ClassMap<WorkTypes> {
        public WorkTypesMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            Map(x => x.BackgroundColor);
            Map(x => x.TextColor);
            Map(x => x.NeedsFields);
            References(x => x.Figure).Cascade.All().Not.LazyLoad();
            HasMany(x => x.Works).Inverse().Cascade.All();

        }
    }
}
