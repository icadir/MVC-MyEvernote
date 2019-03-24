using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.Abstract;

namespace MyEvernote.DataAccessLayer.MySql
{
    public class Repository<T> : RepositoryBase,IRepository<T> where T : class
    {
        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public int Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> @where)
        {
            throw new NotImplementedException();
        }
    }
}
