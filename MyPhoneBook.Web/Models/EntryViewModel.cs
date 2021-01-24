using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhoneBook.Web.Models
{
    public class EntryViewModel : BaseViewModel
    {
        [Required]
        public int PhoneBookId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public List<PhoneBookViewModel> PhoneBooks { get; set; }
    }
}
