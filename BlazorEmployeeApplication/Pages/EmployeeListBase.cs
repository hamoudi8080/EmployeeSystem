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
        private bool isLoggedIn;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

		public bool ShowFooter { get; set; } = true;

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

        public void NavigateToNewPage()
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
		 
           

        
       
	}

}
