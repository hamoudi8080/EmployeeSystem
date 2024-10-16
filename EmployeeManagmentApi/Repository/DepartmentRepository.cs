using EmployeeManagmentApi.Models;
using EmployeeManagmentModel;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagmentApi.Repository
{

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger<DepartmentRepository> _logger;
        public DepartmentRepository(AppDbContext context, ILogger<DepartmentRepository> logger)
        {
            appDbContext = context;
            _logger = logger;
        }


        public async Task<Department> GetDepartment(int departmentId)
        {
            return await appDbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                return await appDbContext.Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data from the database");
                throw;
            }
        }
    }
}
