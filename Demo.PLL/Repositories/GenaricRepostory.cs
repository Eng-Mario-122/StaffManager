using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.PLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Repositories
{
    public class GenaricRepostory<T>:IGenaricRepository<T> where T:ModelBase
    {
        private readonly AppDbContext _dbContext;

        public GenaricRepostory(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public int Add(T entity)
        {

            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbContext.Find<T> (id);
         //   var department = _dbContext.Departments.Local.Where(D => D.Id==id).FirstOrDefault();
         //   if (department is null)
         //       department=_dbContext.Departments.Where(D => D.Id==id).FirstOrDefault();
          //  return department;

        }

        public IEnumerable<T> GetAll()

                => _dbContext.Set<T>().AsNoTracking().ToList();

    }
}
