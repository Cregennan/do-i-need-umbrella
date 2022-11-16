using DoINeedUmbrellaToday.Weather;
using Newtonsoft.Json;
using System.Text;

namespace DoINeedUmbrellaToday.Services
{
#pragma warning disable CS8618
    //Сервис, определяющий коордианты по городу с помощью сервиса PositionStack
    public class PositionStackLocationService : ILocationService
    {
        /// <summary>
        /// Метод возвращает объект местоположения пользователя
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<UserLocation.Result> GetLocationAsync(string query)
        {
            var result = await LocationQueryAsync(query);

            return new UserLocation.Result
            {
                Latitude = result?.Data?[0]?.Latitude,
                Longitude = result?.Data?[0]?.Longitude,
                CountryName = result?.Data?[0]?.Country,
                CityName = result?.Data?[0]?.County,
                RegionName = result?.Data?[0]?.Region,
            };
        }

        /// <summary>
        /// Метод выполняет запрос к API PositionStack с вводом пользователя
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<Location?> LocationQueryAsync(String query)
        {
            string host = "http://api.positionstack.com/v1/forward";

            string[] _params = new[]
            {
                 $"access_key={_config.GetValue<String>("Secrets:PositionStackSecret")}",
                 $"query={query}",
            };

            //Собираем запрос
            var requestBuilder = new StringBuilder();
            requestBuilder.Append(host);
            requestBuilder.Append("?");
            requestBuilder.Append(String.Join("&", _params));

            var response = await _client.GetAsync(requestBuilder.ToString());

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Location>(await response.Content.ReadAsStringAsync());

        }


        private HttpClient _client;
        private IConfiguration _config;


        public PositionStackLocationService(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _config = configuration;
        }


        //Данные классы являются представлением JSON схемы сервиса PositionStack. При выборе другого поставщика данных о местоположении схема а значит и эти классы будут отличатся.
        public partial class Location
        {
            [JsonProperty("data")]
            public Datum[] Data { get; set; }
        }

        public partial class Datum
        {
            [JsonProperty("latitude")]
            public decimal Latitude { get; set; }

            [JsonProperty("longitude")]
            public decimal Longitude { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("number")]
            public object Number { get; set; }

            [JsonProperty("postal_code")]
            public object PostalCode { get; set; }

            [JsonProperty("street")]
            public object Street { get; set; }

            [JsonProperty("confidence")]
            public decimal Confidence { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("region_code")]
            public string RegionCode { get; set; }

            [JsonProperty("county")]
            public string County { get; set; }

            [JsonProperty("locality")]
            public string Locality { get; set; }

            [JsonProperty("administrative_area")]
            public object AdministrativeArea { get; set; }

            [JsonProperty("neighbourhood")]
            public object Neighbourhood { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("country_code")]
            public string CountryCode { get; set; }

            [JsonProperty("continent")]
            public string Continent { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }
        }



#pragma warning restore CS8618
    }
}
