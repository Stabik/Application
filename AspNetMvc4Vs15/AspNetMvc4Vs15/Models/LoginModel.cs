using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMvc4Vs15
{
    public class LoginModel
    {
        public  int Id { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
    }
}