using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PLL.Interfaces
{
    public interface IEmployeeRepository:IGenaricRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByAdress(string adress);
        IQueryable<Employee> GetEmployeeByName(string name);

    }
}
