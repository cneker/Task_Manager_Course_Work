using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using coursework.Models;

namespace coursework.Services
{
    public class UserService
    {
        private readonly string _url = "http://192.168.100.3:5000/api/users";

        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
            
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<User> Log(User user)
        {
            HttpClient client = GetClient();
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_url + "/login", content);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<User> Reg(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/registration",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<User> Get(int id)
        {
            HttpClient client = GetClient();
            var json = JsonSerializer.Serialize(id);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.GetAsync(_url + "/get?" + $"id={id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), _options);
        }
    }
}
