using EmployeeManagmentModel;
using System.Net.Http.Json;




namespace BlazorEmployeeApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Employee[]>("api/employees");
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception here. You might want to log it or take other actions.
                // For now, rethrowing the exception is used for demonstration purposes.
                throw new Exception("Error while fetching employees", ex);
            }
        }



        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
        }
    }
}
