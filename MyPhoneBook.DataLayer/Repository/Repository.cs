using MyPhoneBook.DataLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneBook.DataLayer.Repository
{
    public class Repository<IEntity> : IRepository<IEntity> where IEntity : class
    {
        protected readonly MyLittleBlackBookContext _context;
        public Repository(MyLittleBlackBookContext context)
        {
            _context = context;
        }
        public void Add(IEntity entity)
        {
            _context.Set<IEntity>().Add(entity);
        }

        public IEnumerable<IEntity> GetAll()
        {
            return _context.Set<IEntity>().ToList();
        }

        public IEntity Get(int id)
        {
            return _context.Set<IEntity>().Find(id);
        }

        public void Remove(IEntity entity)
        {
            _context.Set<IEntity>().Remove(entity);
        }
    }
}
