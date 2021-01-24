namespace MyLittleBlackBook.DataLayer.Entity
{
    public class Entry : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public PhoneBook PhoneBook { get; set; }
    }
}
