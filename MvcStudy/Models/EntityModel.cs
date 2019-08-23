using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudy.Models
{
    public class EntityModel<T>
    {
        public T Entity { get; set; }
    }
}