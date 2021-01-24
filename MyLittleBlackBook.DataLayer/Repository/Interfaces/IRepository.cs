using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleBlackBook.DataLayer.Repository.Interfaces
{
    public interface IRepository<IEntity> where IEntity : class
    {
        void Add(IEntity entity);
        void Remove(IEntity entity);
        IEntity Get(int id);
        IEnumerable<IEntity> GetAll();
    }
}
