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
    public class ToDoService
    {
        private readonly string _url = "http://192.168.100.3:5000/api/todo";

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

        public async Task<Models.ToDoList> Delete(int id)
        {
            using HttpClient client = GetClient();
            var response = await client.DeleteAsync(_url + $"/delete?Id={id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.ToDoList>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Models.ToDoList> Add(ToDoList toDo)
        {
            using HttpClient client = GetClient();
            var json = JsonSerializer.Serialize(toDo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_url + $"/create", content);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.ToDoList>(
                await response.Content.ReadAsStringAsync(), _options);
        }
    }
}
