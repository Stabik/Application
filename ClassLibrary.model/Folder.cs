using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model
{
   public class Folder
    {
        public virtual int Id { get; set; }
        public virtual string NameFolder { get; set; }
        public virtual IList<Version> Version { get; set; }
        public virtual RightAccess MyRightAccess { get; set; }
    }
    public class FolderMap : ClassMap<Folder>
    {
        public FolderMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("100");
            Map(x => x.NameFolder).Length(50);
            HasMany(x => x.Version).Inverse();
            References(x => x.MyRightAccess).Cascade.SaveUpdate();

        }
    }
}
