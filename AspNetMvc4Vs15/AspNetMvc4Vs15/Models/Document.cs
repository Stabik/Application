using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc4Vs15
{
    public class Document
    {

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        private MyFile _File;
        public virtual MyFile MyDoc
        {
            get { return _File ?? (_File = new MyFile()); }
            set { _File = value; }
        }
        public virtual Person MyUser { get; set; }

    }
}