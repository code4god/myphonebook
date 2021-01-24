using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyPhoneBook.DataLayer;
using MyPhoneBook.DataLayer.Entity;
using MyPhoneBook.DataLayer.Repository;

namespace Test
{
    class Program
    {
        static MyLittleBlackBookContext _context;
        public Program(MyLittleBlackBookContext context)
        {
            _context = context;
        }
        static void Main(string[] args)            
        {
            using (var ctx = new UnitOfWork(_context))
            {
                var book = new PhoneBook()
                {
                    Name = "Bill",
                    Entries = new List<Entry>
                    {
                        new Entry
                        {
                            Name = "Gabriella",
                            PhoneNumber = "021989898"
                        }
                    }
                };

                ctx.PhoneBooks.Add(book);
                ctx.Complete();
            }
        }
    }
}
