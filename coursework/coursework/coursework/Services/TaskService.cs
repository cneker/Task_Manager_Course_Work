using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace coursework.Services
{
    public class TaskService
    {
        private readonly string _url = "http://192.168.100.3:5000/api/tasks";

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

        public async Task<Models.Task> Get(int id)
        {
            using HttpClient client = GetClient();
            var response = await client.GetAsync(_url + $"/get?Id={id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Models.Task> Add(Models.Task task)
        {
            using HttpClient client = GetClient();
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_url + "/create", content);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Models.Task> Update(Models.Task task)
        {
            using HttpClient client = GetClient();
            var response = await client.PutAsync(_url + "/update",
                new StringContent(
                    JsonSerializer.Serialize(task),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }
        
        public async Task<Models.Task> Delete(int id)
        {
            using HttpClient client = GetClient();
            var response = await client.DeleteAsync(_url + $"/delete?Id={id}");

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Models.Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<IEnumerable<Models.Task>> GetAllUserTasks(string email)
        {
            var client = GetClient();
            var response = await client.GetAsync(_url + $"/user_tasks?Email={email}");

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<IEnumerable<Models.Task>>(
                await response.Content.ReadAsStringAsync(), _options);
        }
    }
}
