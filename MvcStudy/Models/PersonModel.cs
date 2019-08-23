using ClassLibrary.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcStudy.Models
{
    public class PersonModel : EntityModel<Person>
    {
        [Required]
        [DisplayName("Полное имя")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение")]
        public string ConfirmPassword { get; set; }

        public Group group;
        public PersonModel()
        {
              group = new Group();
    }
    }
}