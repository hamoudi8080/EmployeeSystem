using EmployeeManagmentModel;
using Microsoft.EntityFrameworkCore;
 

namespace EmployeeManagmentApi.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Seed Departments Table
			modelBuilder.Entity<Department>().HasData(
				new Department { DepartmentId = 1, DepartmentName = "IT" });
			modelBuilder.Entity<Department>().HasData(
				new Department { DepartmentId = 2, DepartmentName = "HR" });
			modelBuilder.Entity<Department>().HasData(
				new Department { DepartmentId = 3, DepartmentName = "Payroll" });
			modelBuilder.Entity<Department>().HasData(
				new Department { DepartmentId = 4, DepartmentName = "Admin" });

			// Seed Employee Table
			modelBuilder.Entity<Employee>().HasData(new Employee
			{
				EmployeeId = 1,
				FirstName = "John",
				LastName = "Hastings",
				Email = "David@pragimtech.com",
				DateOfBrith = DateTime.SpecifyKind(new DateTime(1980, 10, 5), DateTimeKind.Utc),
                Gender = Gender.Male,
				DepartmentId = 1,
				PhotoPath = "images/john.png"
			});

			modelBuilder.Entity<Employee>().HasData(new Employee
			{
				EmployeeId = 2,
				FirstName = "Sam",
				LastName = "Galloway",
				Email = "Sam@pragimtech.com",
				DateOfBrith = DateTime.SpecifyKind(new DateTime(2001, 10, 5), DateTimeKind.Utc),
                Gender = Gender.Male,
				DepartmentId = 2,
				PhotoPath = "images/sam.jpg"
			});

			modelBuilder.Entity<Employee>().HasData(new Employee
			{
				EmployeeId = 3,
				FirstName = "Mary",
				LastName = "Smith",
				Email = "mary@pragimtech.com",
				DateOfBrith = DateTime.SpecifyKind(new DateTime(1984, 10, 5), DateTimeKind.Utc),
                Gender = Gender.Female,
				DepartmentId = 1,
				PhotoPath = "images/mary.png"
			});

			modelBuilder.Entity<Employee>().HasData(new Employee
			{
				EmployeeId = 4,
				FirstName = "Sara",
				LastName = "Longway",
				Email = "sara@pragimtech.com",
				DateOfBrith = DateTime.SpecifyKind(new DateTime(1986, 10, 5), DateTimeKind.Utc),
                Gender = Gender.Female,
				DepartmentId = 3,
				PhotoPath = "images/sara.png"
			});
		}
	}
}
