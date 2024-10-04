using BlazorEmployeeApplication.Services;
using Microsoft.AspNetCore.Components;


namespace BlazorEmployeeApplication.Pages
{
    public class LoginBase : ComponentBase
    {
        protected string userName;
        protected string password;
        protected string errorLabel;


        [Inject]
        public IAuthService authService { get; set; }

        [Inject]
        public NavigationManager navMgr { get; set; }

        protected async Task LoginAsync()
        {
            errorLabel = "";
            try
            {
                await authService.LoginAsync(userName, password);
                navMgr.NavigateTo("/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                errorLabel = $"Error: {e.Message}";
            }
        }


        protected void Login()
        {
            navMgr.NavigateTo("/Login");
        }

        protected async Task Logout()
        {
            await authService.LogoutAsync();
            navMgr.NavigateTo("/");
        }



    }
}
