using MyPhoneBook.DataLayer.Entity;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneBook.DataLayer.Repository
{
    public class EntryRepository : Repository<Entry>, IEntryRepository
    {
        public EntryRepository(MyLittleBlackBookContext context) : base(context)
        {
        }

        public IEnumerable<Entry> GetAll(int phoneBookId)
        {
            return _context.Entries.Where(entry=> entry.PhoneBookId == phoneBookId);
        }
    }
}
