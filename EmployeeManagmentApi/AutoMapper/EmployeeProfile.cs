using AutoMapper;
using EmployeeManagmentModel;

namespace EmployeeManagmentApi.AutoMapper
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            // Maps from Employee to Employee and vice versa
            CreateMap<Employee, Employee>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
