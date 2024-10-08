using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManagmentModel
{
	public class Department
	{
		public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }

        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; }

    }
}
