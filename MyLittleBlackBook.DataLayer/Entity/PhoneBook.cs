using System.Collections.Generic;

namespace MyLittleBlackBook.DataLayer.Entity
{
    public class PhoneBook : BaseEntity
    {
        public List<Entry> Entries { get; set; }
        
    }
}
