using MyLittleBlackBook.DataLayer.Entity;
using MyLittleBlackBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleBlackBook.DataLayer.Repository
{
    public class PhoneBookRepository : Repository<PhoneBook>, IPhoneBookRepository
    {
        public PhoneBookRepository(MyLittleBlackBookContext context) : base(context)
        {
            
        }
    }
}
