using EmployeeManagmentModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;


namespace EmployeeManagmentApi.Models
{
    public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}
        // "DefaultConnection": "Host=employeepostgressqldb.postgres.database.azure.com; Port=5432; Database=EmployeesDB; Username=techschool;Password=Aqaq1997914"
        public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure Admin entity
			modelBuilder.Entity<Admin>()
				.HasKey(a => a.AdminId);


            modelBuilder.Entity<Admin>()
		   .HasMany(e => e.Employees)
		   .WithOne(e => e.Admin)
		   .HasForeignKey(e => e.AdminId)
		   .HasPrincipalKey(e => e.AdminId);

            // Configure Employee entity
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);
           
            modelBuilder.Entity<Employee>()
			.HasOne(e => e.Department)
			.WithMany(d => d.Employees)
			.HasForeignKey(e => e.DepartmentId);

            // Seed Departments Table
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 2, DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 3, DepartmentName = "Payroll" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 4, DepartmentName = "Admin" });

            // Seed Admin Table
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Age = 36,
                    Email = "trmo@via.dk",
                    Domain = "via",
                    Name = "hamo",
                    Password = "1234",
                    Role = "Teacher",
                    Username = "hamo"
                });

            // Seed Employees Table
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "John",
                    LastName = "Hastings",
                    Email = "David@pragimtech.com",
                    DateOfBrith = DateTime.SpecifyKind(new DateTime(1980, 10, 5), DateTimeKind.Utc),
                    Gender = Gender.Male,
                    DepartmentId = 1,
                    PhotoPath = "images/john.png",
                    AdminId = 1  // Foreign key to Admin
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Sam",
                    LastName = "Galloway",
                    Email = "Sam@pragimtech.com",
                    DateOfBrith = DateTime.SpecifyKind(new DateTime(2001, 10, 5), DateTimeKind.Utc),
                    Gender = Gender.Male,
                    DepartmentId = 2,
                    PhotoPath = "images/sam.jpg",
                    AdminId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Mary",
                    LastName = "Smith",
                    Email = "mary@pragimtech.com",
                    DateOfBrith = DateTime.SpecifyKind(new DateTime(1984, 10, 5), DateTimeKind.Utc),
                    Gender = Gender.Female,
                    DepartmentId = 1,
                    PhotoPath = "images/mary.png",
                    AdminId = 1
                },
                new Employee
                {
                    EmployeeId = 4,
                    FirstName = "Sara",
                    LastName = "Longway",
                    Email = "sara@pragimtech.com",
                    DateOfBrith = DateTime.SpecifyKind(new DateTime(1986, 10, 5), DateTimeKind.Utc),
                    Gender = Gender.Female,
                    DepartmentId = 3,
                    PhotoPath = "images/sara.png",
                    AdminId = 1
                });

        }
    }
}
