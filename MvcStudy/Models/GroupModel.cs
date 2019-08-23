using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcStudy.Models
{
    public class GroupModel
    {
        [DisplayName("Название группы")]
        public string Login { get; set; }
    }
}