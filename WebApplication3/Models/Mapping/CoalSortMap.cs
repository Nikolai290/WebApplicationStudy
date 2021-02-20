using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class CoalSortMap : ClassMap<CoalSort> {
        public CoalSortMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            HasMany(x => x.Works).Inverse().Cascade.All();

        }
    }
}
