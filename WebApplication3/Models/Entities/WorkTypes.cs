using System.Collections.Generic;

namespace WebApplication3.Models.Entities {
    public class WorkTypes : DbEntities {
        public virtual string Name { get; protected set; }
        public virtual string BackgroundColor { get; protected set; }
        public virtual string TextColor { get; protected set; }
        public virtual Figure Figure { get; protected set; }
        public virtual IList<Work> Works { get; protected set; } = new List<Work>();

        public WorkTypes() { }

        public WorkTypes(string name, string backColor, string textColor, Figure figure) {
            Name = name;
            BackgroundColor = backColor;
            TextColor = textColor;
            Figure = figure;
            //figure.Type.Add(this);
        }

        public virtual WorkTypes SetId(int id) {
            Id = id > 0 ? id : 0;
            return this;
        }

    }
}
