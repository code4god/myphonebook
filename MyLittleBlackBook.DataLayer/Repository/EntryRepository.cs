﻿using MyLittleBlackBook.DataLayer.Entity;
using MyLittleBlackBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleBlackBook.DataLayer.Repository
{
    public class EntryRepository : Repository<Entry>, IEntryRepository
    {
        public EntryRepository(MyLittleBlackBookContext context) : base(context)
        {
        }
    }
}
