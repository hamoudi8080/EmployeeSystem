using Shared.Dtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using BlazorEmployeeApplication.Auth;
using Microsoft.JSInterop;
using Blazored.SessionStorage;
using EmployeeManagmentModel;



namespace BlazorEmployeeApplication.Services
{

    public class JwtAuthService : IAuthService
    {


        private readonly HttpClient httpClient;

        private ISessionStorageService _SessionStorage;

        // this private variable for simple caching
        public string? Jwt { get; private set; } = "";

        public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

        public JwtAuthService(HttpClient httpClient, ISessionStorageService _SessionStorage)
        {
            this.httpClient = httpClient;
            this._SessionStorage = _SessionStorage;
        }
       

        public async Task<string> LoginAsync(string username, string password)
        {
            UserLoginDto userLoginDto = new()
            {
                Username = username,
                Password = password
            };

            string userAsJson = JsonSerializer.Serialize(userLoginDto);
            StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"api/Auth/login", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            string token = responseContent;
            //Jwt = token;

            await _SessionStorage.SetItemAsync("token", token);

            Jwt = await _SessionStorage.GetItemAsync<string>("token");
            ClaimsPrincipal principal = await CreateClaimsPrincipalAsync();

            OnAuthStateChanged.Invoke(principal);
            return token;
        }

        private async Task<ClaimsPrincipal> CreateClaimsPrincipalAsync()
        {
            Jwt = await _SessionStorage.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(Jwt))
            {
                return new ClaimsPrincipal();
            }

            IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

            ClaimsIdentity identity = new(claims, "Jwt");

            ClaimsPrincipal principal = new(identity);
            return principal;
        }

        public async Task<Task> LogoutAsync()
        {
            Jwt = null;
             await _SessionStorage.RemoveItemAsync("token");
            ClaimsPrincipal principal = new();
            OnAuthStateChanged.Invoke(principal);
          
            return Task.CompletedTask;
        }

        public async Task RegisterAsync(Admin user)
        {
            string userAsJson = JsonSerializer.Serialize(user);
            StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7130/auth/register", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }
        }

        public async Task<ClaimsPrincipal> GetAuthAsync()
        {
            ClaimsPrincipal principal = await CreateClaimsPrincipalAsync();
            return await Task.FromResult(principal);
        }


        // Below methods stolen from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            string payload = jwt.Split('.')[1];
            byte[] jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
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
    }
}
