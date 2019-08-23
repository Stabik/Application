
using ClassLibrary.model;
using FluentNHibernate.Mapping;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model
{
    public class Group
    {
        public virtual int Id { get; set; }
        public virtual string NameGroup { get; set; }
        public virtual RightAccess MyRightAccess { get; set; }

        public virtual IList<Person> PersonList { get; set; }
        public Group()
        {
            PersonList = new List<Person>();
        }
    }

    public class GroupMap : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("100");
            Map(x => x.NameGroup).Length(50);
            HasMany(x => x.PersonList).Inverse();
            References(x => x.MyRightAccess).Cascade.SaveUpdate();
        }
    }
}

