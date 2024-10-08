using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManagmentModel
{
    public class Employee
	{
		public int EmployeeId { get; set; }
		[Required]
		[MinLength(2)]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }

		
		public string Email { get; set; }
		public DateTime DateOfBrith { get; set; }
		public Gender Gender { get; set; }
        public string PhotoPath { get; set; }

       
        public int DepartmentId { get; set; }


        [JsonIgnore]
        public Department? Department { get; set; }


       
        public int? AdminId { get; set; }
        [JsonIgnore]
        public Admin? Admin { get; set; }
	}
}
