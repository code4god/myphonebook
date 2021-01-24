using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MyPhoneBook.API.Model
{
    public class Entry : BaseModel
    {
        public int PhoneBookId { get; set; }
        public string PhoneNumber { get; set; }
        //public PhoneBook PhoneBook { get; set; }
    }
}
