using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkillProfiWebApplication
{
    public class RequestServiceApi
    {
        private HttpClient httpClient { get; set; }

        string baseUrl = "http://localhost:5000/requests/";

        HttpResponseMessage response;

        Random rnd = new Random();

        public RequestServiceApi()
        {
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            response = await httpClient.GetAsync(baseUrl + "getall");

            return JsonSerializer.Deserialize<IEnumerable<Request>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IEnumerable<Request>> GetRequests(RequestFilter requestFilter)
        {
            response = await httpClient.PostAsync(baseUrl + "get", new StringContent(JsonSerializer.Serialize(requestFilter), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<IEnumerable<Request>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<string> AddRequest(Request request)
        {
            response = await httpClient.PostAsync(baseUrl + "add", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

            return response.StatusCode.ToString();
        }

        public async Task<Request> FindRequest(int id)
        {
            response = await httpClient.PostAsync(baseUrl + "find", new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<Request>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<string> ChangeRequestStatus(Request request)
        {
            response = await httpClient.PostAsync(baseUrl + "changerequeststatus", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
            return response.StatusCode.ToString();
        }


    }
}
