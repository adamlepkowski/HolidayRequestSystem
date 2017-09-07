using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HolidayRequestSystem.Domain.Utils
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        void Add(T entity);
    }
}
