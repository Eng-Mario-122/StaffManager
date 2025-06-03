using Demo.DAL.Data;
using Demo.DAL.Models;
using Demo.PLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Repositories
{
    public class EmployeeRepository:GenaricRepostory<Employee>,IEmployeeRepository

    {
        private AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) 
        { 
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeeByAdress(string adress)
        {
            return _dbContext.Employees.Where(E=>E.Adress.ToLower().Contains(adress.ToLower()));    
        }

      
        public IQueryable<Employee> GetEmployeeByName(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

        }
    }
}
