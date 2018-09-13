using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using Plugin.Connectivity;

using DongdaCS.Models;
using JsonApiSerializer;

namespace DongdaCS.Services {
    public class PhMaxJobStore : IDataStore<PhMaxJob> {

        HttpClient client;
        IEnumerable<PhMaxJob> items;

        public PhMaxJobStore() {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.100.174:8080/api/v1/");

            items = new List<PhMaxJob>();
        }

        public async Task<bool> AddItemAsync(PhMaxJob item) {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id) {
            throw new NotImplementedException();
        }

        public async Task<PhMaxJob> GetItemAsync(string id) {
            if (id != null && CrossConnectivity.Current.IsConnected) {
                var request = new HttpRequestMessage() {
                    RequestUri = new Uri("http://192/168/100/174:8080/api/v1/tmp/0"),
                    Method = HttpMethod.Post,
                };
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Content-Type", "application/json");
                request.Headers.Add("Authorization", "bearer 5c138ef2ffc97ba8e165b5a8b256df71");
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                PhMaxJob[] jobs = JsonConvert.DeserializeObject<PhMaxJob[]>(json, new JsonApiSerializerSettings());
                System.Console.WriteLine(jobs);
                return null;
            }

            return null;
        }

        public async Task<IEnumerable<PhMaxJob>> GetItemsAsync(bool forceRefresh = false) {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(PhMaxJob item) {
            throw new NotImplementedException();
        }
    }
}
