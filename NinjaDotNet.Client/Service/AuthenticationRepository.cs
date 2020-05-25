using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using NinjaDotNet.Client.Contracts;
using NinjaDotNet.Client.Models;
using NinjaDotNet.Client.Providers;
using NinjaDotNet.Client.Static;

namespace NinjaDotNet.Client.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authState;

        public AuthenticationRepository(IHttpClientFactory client, ILocalStorageService localStorage,
            AuthenticationStateProvider authState)
        {
            _client = client;
            _localStorage = localStorage;
            _authState = authState;
        }

        public async Task<bool> Register(RegistrationModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.RegisterEndpoint);
            if (user == null)
                return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(LoginModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.LoginEndpoint);
            if (user == null)
                return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(content);

                // Store the Token
                await _localStorage.SetItemAsync("authToken", token.Token);
                // Change auth state of the app
                await ((ApiAuthenticationStateProvider) _authState).LoggedIn();

                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("bearer", token.Token);

                return true;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authState).LoggedOut();
        }
    }
}
