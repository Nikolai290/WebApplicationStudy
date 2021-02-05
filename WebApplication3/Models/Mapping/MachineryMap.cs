using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class MachineryMap : ClassMap<Machinery> {

        public MachineryMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            
        }
    }
}
