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
    public class DepartmentRepository : GenaricRepostory<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext) :base(dbContext)
        {

        }
    }
}
