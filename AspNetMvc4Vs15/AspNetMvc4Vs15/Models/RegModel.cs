using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetMvc4Vs15
{
    public class RegModel
    {
        
            public  int Id { get; set; }
            [Required]
            public  string Name { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public  string Password { get; set; }
       
    }
}