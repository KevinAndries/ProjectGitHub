using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class LoginViewModel
    {
        public string UserLogin { get; set; }
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
