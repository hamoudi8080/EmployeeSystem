using BlazorEmployeeApplication.Services;
using EmployeeManagmentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorEmployeeApplication.Pages
{

    public class EmployeeListBase : ComponentBase
    {

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; } = null!;

        private string? name;
        private IEnumerable<Claim>? userClaims;
        protected bool isLoggedIn;

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        protected IEnumerable<Employee> Employees { get; set; }

        protected bool ShowFooter { get; set; } = true;

		protected override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await AuthState;
            ClaimsPrincipal user = authState.User;
            isLoggedIn = user.Identity != null;

            if (!isLoggedIn) return;

            userClaims = user.Claims;
            name = user.Identity!.Name!;
            Employees = (await EmployeeService.GetEmployees()).ToList();

             
        }

        protected void NavigateToNewPage()
        {
            NavigationManager.NavigateTo("/newpage");
        }

        protected int SelectedEmployeesCount { get; set; } = 0;

        protected void EmployeeSelectionChanged(bool isSelected)
		{
			if (isSelected)
			{
				SelectedEmployeesCount++;
			}
			else
			{
				SelectedEmployeesCount--;
			}
		}

        protected Task AddEmployee()
        {
            NavigationManager.NavigateTo("/editemployee");
            return Task.CompletedTask;
        }





    }

}
