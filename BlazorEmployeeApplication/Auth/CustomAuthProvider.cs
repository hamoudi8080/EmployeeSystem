using Blazored.SessionStorage;
using BlazorEmployeeApplication.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;


namespace BlazorEmployeeApplication.Auth
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        private readonly IAuthService authService;
        private readonly ISessionStorageService sessionStorageService;

        public CustomAuthProvider(IAuthService authService, ISessionStorageService sessionStorageService)
        {
            this.authService = authService;
            this.sessionStorageService = sessionStorageService;
            authService.OnAuthStateChanged += AuthStateChanged;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = await authService.GetAuthAsync();
            return new AuthenticationState(principal);
        }

        public async Task LoadUserStateAsync()
        {
            var token = await sessionStorageService.GetItemAsStringAsync("token");
            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "CustomAuthType");
            }
            else
            {
                identity = new ClaimsIdentity(); // No token, create an anonymous identity
            }

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }

        private void AuthStateChanged(ClaimsPrincipal principal)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
    }
}
