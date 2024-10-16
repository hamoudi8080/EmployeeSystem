using EmployeeManagmentApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    public class EmployeeTest
    {
         public static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");
            return builder.Options;
        }
    }
}
