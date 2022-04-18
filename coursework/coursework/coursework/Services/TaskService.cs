﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using coursework.Models;
using Task = System.Threading.Tasks.Task;

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

        public async Task<Task> Get(int id)
        {
            HttpClient client = GetClient();
            var json = JsonSerializer.Serialize(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_url + "/get", content);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Task> Add(Task task)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/create",
                new StringContent(
                    JsonSerializer.Serialize(task),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }

        public async Task<Task> Update(Task task)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/update",
                new StringContent(
                    JsonSerializer.Serialize(task),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }
        //may be remove return Task value
        public async Task<Task> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(_url + "/delete",
                new StringContent(
                    JsonSerializer.Serialize(id),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Task>(
                await response.Content.ReadAsStringAsync(), _options);
        }
    }
}