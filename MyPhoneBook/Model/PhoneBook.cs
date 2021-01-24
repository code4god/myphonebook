using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MyPhoneBook.API.Model
{
    
    public class PhoneBook : BaseModel
    {
        public List<Entry> Entries { get; set; }
    }
}
