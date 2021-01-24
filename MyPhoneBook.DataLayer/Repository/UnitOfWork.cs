using MyPhoneBook.DataLayer.Entity;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhoneBook.DataLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyLittleBlackBookContext _context;
        public UnitOfWork(MyLittleBlackBookContext context)
        {
            _context = context;
            PhoneBooks = new PhoneBookRepository(_context);
            Entries = new EntryRepository(_context);
        }

        public IPhoneBookRepository PhoneBooks { get;  set; }
        public IEntryRepository Entries { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
