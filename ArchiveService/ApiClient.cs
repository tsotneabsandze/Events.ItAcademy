using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArchiveService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ArchiveService
{
    public class ApiClient
    {
        private readonly ILogger<ApiClient> _logger;
        private static HttpClient _client;

        public ApiClient(ILogger<ApiClient> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task EvaluateEvents()
        {
            var events = await GetList();
            foreach (var record in events)
            {
                if (!record.IsArchived && record.Ends < DateTime.Now)
                    await AddToArchive(record.Id);
                        
            }
        }

        private async Task<List<PartialEventDto>> GetList()
        {
            var response =
                await _client.GetAsync($"{Constants.BaseApiUrl}/Events/GetFullList");
            var dataString = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<List<PartialEventDto>>(dataString);
            return events;
        }

        private async Task AddToArchive(int eventId)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(new { Id = eventId }),
                    Encoding.UTF8, Constants.ContentType);

                await _client.PutAsync($"{Constants.BaseApiUrl}/Events/AddToArchive",
                    stringContent);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}