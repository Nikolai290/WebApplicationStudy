﻿using FluentNHibernate.Mapping;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.Mapping {
    public class QuarryAreaMap : ClassMap<QuarryArea> {
        public QuarryAreaMap() {
            Id(x => x.Id);
            Map(x => x.IsDelete);
            Map(x => x.Name);
            HasMany(x => x.MachineryOnShift)
                .Inverse()
                .Cascade.All();
        }
    }
}
