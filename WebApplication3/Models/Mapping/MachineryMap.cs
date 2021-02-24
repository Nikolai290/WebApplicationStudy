using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class MachineryMap : ClassMap<Machinery> {

        public MachineryMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            References(x => x.Type).Not.LazyLoad().Cascade.All();
        }
    }
}
