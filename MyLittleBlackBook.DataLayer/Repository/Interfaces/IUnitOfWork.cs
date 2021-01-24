﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyLittleBlackBook.DataLayer.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IPhoneBookRepository PhoneBooks { get; set; }
        public IEntryRepository Entries { get; set; }
        int Complete();
    }
}
