using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using coursework.Models;

namespace coursework.Services
{
    public class UserService
    {
        private readonly string _url = "http://localhost:18965/api/tasks";

        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = GetClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<User> Get(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/get",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<User> Add(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/create",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), _options);
        }
    }
}
