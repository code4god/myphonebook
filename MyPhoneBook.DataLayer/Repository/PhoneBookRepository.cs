using MyPhoneBook.DataLayer.Entity;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneBook.DataLayer.Repository
{
    public class PhoneBookRepository : Repository<PhoneBook>, IPhoneBookRepository
    {
        public PhoneBookRepository(MyLittleBlackBookContext context) : base(context)
        {
            
        }
    }
}
