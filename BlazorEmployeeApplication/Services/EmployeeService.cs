using EmployeeManagmentModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;




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

         public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                // Serialize the updatedEmployee object to JSON
                var content = new StringContent(JsonSerializer.Serialize(updatedEmployee), Encoding.UTF8, "application/json");

                // Send the HTTP PUT request to update the employee
                var response = await httpClient.PutAsync("api/employees", content);

                // Check if the request was successful
                response.EnsureSuccessStatusCode();

                // Deserialize and return the updated employee
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception here. You might want to log it or take other actions.
                // For now, rethrowing the exception is used for demonstration purposes.
                throw new Exception("Error while updating employee", ex);
            }
        }
        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/employees", newEmployee);

            if (response.IsSuccessStatusCode)
            {
                // Read and return the Employee from the response
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            else
            {
                // Handle the error or return null, depending on your requirements
                return null;
            }
        }





    }


}
