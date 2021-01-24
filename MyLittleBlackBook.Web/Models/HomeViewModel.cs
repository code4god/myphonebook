using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleBlackBook.Web.Models
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Please complete name!")]
        public string Name { get; set; }

    }
}
