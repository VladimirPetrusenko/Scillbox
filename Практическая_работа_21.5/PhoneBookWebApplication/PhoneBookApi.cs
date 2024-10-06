using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace PhoneBookWebApplication
{
    public class PhoneBookApi
    {
        private HttpClient httpClient { get; set; }

        string baseUrl = "http://localhost:5000/phonebook/";

        HttpResponseMessage response;

        Random rnd = new Random();

        public PhoneBookApi()
        {
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PhoneBook>> GetPhoneBookEntry()
        {
            response = await httpClient.GetAsync(baseUrl + "get");

            return JsonSerializer.Deserialize<IEnumerable<PhoneBook>>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<string> AddPhoneBookEntry(PhoneBook phoneBook)
        {
            response = await httpClient.PostAsync(baseUrl + "add", new StringContent(JsonSerializer.Serialize(phoneBook), Encoding.UTF8, "application/json"));

            return response.StatusCode.ToString();
        }

        public async Task<PhoneBook> FindPhoneBookEntry(int id)
        {
            response = await httpClient.PostAsync(baseUrl + "find", new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

            return JsonSerializer.Deserialize<PhoneBook>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<string> DeletePhoneBookEntry(PhoneBook phoneBook)
        {
            response = await httpClient.PostAsync(baseUrl + "delete", new StringContent(JsonSerializer.Serialize(phoneBook), Encoding.UTF8, "application/json"));

            return response.StatusCode.ToString();
        }

        public async Task<string> EditPhoneBookEntry(PhoneBook phoneBook)
        {
            response = await httpClient.PostAsync(baseUrl + "edit", new StringContent(JsonSerializer.Serialize(phoneBook), Encoding.UTF8, "application/json"));

            return response.StatusCode.ToString();
        }
    }
}
