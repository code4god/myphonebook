using MyLittleBlackBook.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleBlackBook.DataLayer.Repository.Interfaces
{
    public interface IEntryRepository : IRepository<Entry>
    {
        // again one can create custom queries here.    
    }
}
