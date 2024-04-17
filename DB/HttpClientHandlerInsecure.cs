using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.DB
{
    public class HttpClientHandlerInsecure : HttpClientHandler
    {
        public HttpClientHandlerInsecure()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }
    }


    public class RestService
    {
        private static readonly HttpClient httpClient = new HttpClient(new HttpClientHandlerInsecure())
        {
            BaseAddress = new Uri("https://localhost:7177/api/"),
        };


        public async Task<List<Plant>> GetPlantsAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Plant>>("Plants");
        }


        public async Task<Plant> GetPlantAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Plant>($"Plants/{id}");
        }


        public async Task<Plant> CreatePlantAsync(Plant plant)
        {
            using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("Plants", plant))
            {
                return await response.Content.ReadFromJsonAsync<Plant>();
            }
        }


        public async Task<Plant> UpdatePlantAsync(Plant plant)
        {
            using (HttpResponseMessage response = await httpClient.PutAsJsonAsync($"Plants/{plant.Id}", plant))
            {
                return await response.Content.ReadFromJsonAsync<Plant>();
            }
        }


        public async Task DeletePlantAsync(int id)
        {
            using (HttpResponseMessage response = await httpClient.DeleteAsync($"Plants/{id}"))
            {
                await response.Content.ReadFromJsonAsync<Plant>();
            }
        }

    }
}
