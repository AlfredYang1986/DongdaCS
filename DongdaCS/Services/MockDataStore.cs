using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using DongdaCS.Models;
using JsonApiSerializer;

namespace DongdaCS {
    public class MockDataStore : IDataStore<Item> {
        List<Item> items;

        public MockDataStore() {
            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is a nice description"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is a nice description"},
            };

            foreach (Item item in _items) {
                items.Add(item);
            }
        }

        public static string HttpPost(string URI, string Parameters) {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            //req.ContentType = "application/x-www-form-urlencoded";
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add("Authorization: bearer 5c138ef2ffc97ba8e165b5a8b256df71");
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public async Task<bool> AddItemAsync(Item item) {
            items.Add(item);

            var req = new request { id = "abc", res = "abc" };
            string req_json = JsonConvert.SerializeObject(req, new JsonApiSerializerSettings());
            string json = HttpPost("http://192.168.100.174:8080/api/v1/tmp/0", req_json);
            PhMaxJob[] jobs = JsonConvert.DeserializeObject<PhMaxJob[]>(json, new JsonApiSerializerSettings());
            System.Console.WriteLine(jobs);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item) {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id) {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id) {
            //return null;

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false) {
            return await Task.FromResult(items);
        }
    }
}
