namespace MyPhoneBook.DataLayer.Entity
{
    public class Entry : BaseEntity
    {
        public int PhoneBookId { get; set; }
        public string PhoneNumber { get; set; }
       // public PhoneBook PhoneBook { get; set; }
    }
}
