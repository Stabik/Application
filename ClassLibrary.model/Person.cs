using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model
{
   public class Person
    {
        public virtual int Id { get; set; }
        
        public virtual string Login{ get; set; }
        public virtual string Password { get; set; }
        public virtual RightAccess MyRightAccess { get; set; }

        public virtual Group Group { get; set; }

        public virtual IList<Version> Version { get; set; }

        public virtual IList<Document> Document { get; set; }
        public Person()
        {
            Group = new Group();
        }
    }
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("100");
            Map(x => x.Login).Length(50);
            Map(x => x.Password).Length(50);
            References(x => x.Group).Cascade.SaveUpdate();
            References(x => x.MyRightAccess).Cascade.SaveUpdate();
            HasMany(x => x.Version).Inverse();
            HasMany(x => x.Document).Inverse();
        }
    }
}
