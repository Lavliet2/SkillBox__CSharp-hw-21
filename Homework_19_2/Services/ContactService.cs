using WebAPIContacts.Models;

namespace Homework_20.Services
{
    public class ContactService
    {
        private readonly HttpClient _httpClient;
        private string _baseAddress = "https://localhost:7214"; // Адрес Web API

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseAddress}/contacts");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<Contact>>();
        }

        public async Task AddContactAsync(Contact contact)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseAddress}/contacts", contact);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseAddress}/contacts/{contact.ID}", contact);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteContactAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseAddress}/contacts/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
