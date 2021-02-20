using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class FigureMap : ClassMap<Figure> {
        public FigureMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            HasMany(x => x.Type).Inverse().Cascade.All();

        }
    }
}
