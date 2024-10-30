using EmployeeManagmentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorEmployeeApplication.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [EmailDomainValidator(AllowedDomain = "pragimtech.com")]
        public string Email { get; set; }
        [CompareProperty("Email",
         ErrorMessage = "Email and Confirm Email must match")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        [ValidateComplexType]

        public Department Department { get; set; } =  new Department();   
        public string PhotoPath { get; set; }
    }
}
