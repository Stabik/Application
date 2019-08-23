using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.model
{
    public class Document : Folder
    {
        public virtual DateTime DateTime { get; set; }
        public virtual Person Author { get; set; }
        public virtual byte[] Buffer { get; set; }
        public virtual string DocType { get; set; }// тип документа
    }
    public class DocumentMap: SubclassMap<Document>
        {
        public DocumentMap()
        {
            References(x => x.Author).Cascade.SaveUpdate();
            Map(x => x.DocType);           
            Map(x => x.Buffer);
            Map(x => x.DateTime);
        }
        }
}
