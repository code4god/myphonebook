using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhoneBook.Web.Models
{
    public class EntryViewModel 
    {
        [Required]
        public int phoneBookId { get; set; }
        public PhoneBook PhoneBook { get; set; }
        public List<Entry> Entries { get; set; }
    }

    public class Entry : BaseViewModel
    {
        public int phoneBookId { get; set; }

        public PhoneBook PhoneBook { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }

    public class PhoneBook : BaseViewModel 
    {
    }
}
