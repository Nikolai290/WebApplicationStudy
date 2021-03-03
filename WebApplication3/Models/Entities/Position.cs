﻿using System.Collections.Generic;
using System;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.Entities {
    public class Position : DbEntities {

        public virtual string Name { get; set; }
        public virtual string Subname { get; set; }
        public virtual IList<Employee> Employees { get; set; }

        public Position() {
            Employees = new List<Employee>();
        }

        public Position(string name) {
            Name = name;
            Subname = name;
        }

        public Position(string name, string subname) : this(name) {
            if(!String.IsNullOrEmpty(subname))
                Subname = subname;
        }

        public override string ToString() {
            return (Name + " " + Subname);
        }

        public virtual void CopyTo(Position upd) {
            upd.Name = this.Name;
            upd.Subname = this.Subname;
            
        }


        public virtual Position Check() {
            if (String.IsNullOrEmpty(Subname))
                Subname = Name;
            return this;
        }

        public virtual Position Create (PositionsAddDTO model) {
            Name = model.Name;
            Subname = model.Subname;
            Employees = model.Employees;

            return this;
        }


    }
}
