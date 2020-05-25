﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NinjaDotNet.Client.Contracts;

namespace NinjaDotNet.Client.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        public BaseRepository(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<bool> Create(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
                return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1)
                return false;
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<T> Get(string url, int id)
        {
            if (id < 1)
                throw new Exception("ID Must be > 1");
            var request = new HttpRequestMessage(HttpMethod.Get, url + id);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;
        }

        public async Task<IList<T>> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }

            return null;
        }

        public async Task<bool> Update(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            if (obj == null)
                return false;
            request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == HttpStatusCode.Created;
        }
    }
}
