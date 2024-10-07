using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentModel
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public int SecurityLevel { get; set; }

        public Collection<Employee> Employees { get; set; }



    }
}
