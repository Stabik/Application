using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc4Vs15
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        private IList<Document> _Doc;
        public virtual IList<Document> UserDoc
        {
            get { return _Doc ?? (_Doc = new List<Document>()); }
            set { _Doc = value; }
        }
        public virtual void AddDoc(Document doc)
        {
            if (_Doc == null)
            {
                _Doc = new List<Document>();
            }
            doc.MyUser = this;
            _Doc.Add(doc);
        }
    }
}