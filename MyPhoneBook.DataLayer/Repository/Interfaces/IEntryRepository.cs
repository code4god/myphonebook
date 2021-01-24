using MyPhoneBook.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneBook.DataLayer.Repository.Interfaces
{
    public interface IEntryRepository : IRepository<Entry>
    {
        // again one can create custom queries here.    
        public IEnumerable<Entry> GetAll(int phoneBookId);
    }
}
