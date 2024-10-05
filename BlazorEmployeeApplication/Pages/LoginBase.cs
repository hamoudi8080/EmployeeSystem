
using BlazorEmployeeApplication.Auth;
using BlazorEmployeeApplication.Services;
using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;
using System.Security.Claims;



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

        //[Inject]
        //public ISessionStorageService SessionStorage { get; set; }

       
     
        protected async Task LoginAsync()
        {
            errorLabel = "";
            try
            {
                string token = await authService.LoginAsync(userName, password);
                if (!string.IsNullOrEmpty(token))
                {
                 //   await SessionStorage.SetItemAsync("token", token);

                    navMgr.NavigateTo("/");
                }


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
    
        //   await SessionStorage.RemoveItemAsync("token");
            await authService.LogoutAsync(); // Call the logout method
           


        }



    }
}
