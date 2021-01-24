using System.Collections.Generic;

namespace MyPhoneBook.DataLayer.Entity
{
    public class PhoneBook : BaseEntity
    {
        public List<Entry> Entries { get; set; }
        
    }
}
