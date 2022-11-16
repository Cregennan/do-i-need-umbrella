using DoINeedUmbrellaToday.Weather;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DoINeedUmbrellaToday.Services
{

#pragma warning disable CS8618
    /// <summary>
    /// Сервис, берущий погодные данные с сайта OpenMeteo
    /// </summary>
    public class OpenMeteoForecastService : IForecastService
    {
        /// <summary>
        /// Получение прогноза на сегодня
        /// </summary>
        /// <param name="location">Местоположение пользователя</param>
        /// <returns>Объект погодных данных</returns>
        /// <exception cref="InvalidLocationException">Исключение выбрасывается в случае если в метод передано неверное местоположение пользователя</exception>
        public async Task<ForecastInfo> GetForecastForToday(UserLocation location)
        {

            if (!location.Validated)
            {
                throw new InvalidLocationException();
            }

            //Получаем данные от OpenMeteo
            WeatherResponse? response = await GetWeatherAPIResponseAsync(location);

            //Упаковываем данные во внутренний формат
            return new ForecastInfo
            {
                //Достаточно этих данных
                WeatherCode = response?.Daily?.Weathercode?[0] ?? 0,
                ShowersSum = response?.Daily?.ShowersSum?[0] ?? 0,
                RainSum = response?.Daily?.RainSum?[0] ?? 0,
                TemperatureMax = response?.Daily?.Temperature2MMax?[0] ?? 0,
                TemperatureMin = response?.Daily?.Temperature2MMin?[0] ?? 0,
            };

        }

        private IConfiguration _configuration;
        private HttpClient _httpClient;

        public OpenMeteoForecastService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _httpClient = client;
        }

        //Отправляем запрос на эндпоинт Open-Meteo для получения погоды на сегодяшний день
        private async Task<WeatherResponse?> GetWeatherAPIResponseAsync(UserLocation location)
        {

            //Open-Meteo даже не требует авторизации токеном. Приятно)
            string Endpoint = "https://api.open-meteo.com/v1/forecast";

            //Лучше собрать запрос так, чем интерполировать строку

            var query = new[]
            {
                "timezone=auto",

                //Требуемые координаты по WGS84 (широта, долгота)
                $"latitude={location.Latitude}",
                $"longitude={location.Longitude}",

                //Дата начала и конца выборки (сегодня)
                $"start_date={DateTime.Now.ToString("yyyy-MM-dd")}",
                $"end_date={DateTime.Now.ToString("yyyy-MM-dd")}",

                 //Требуемые данные тщательно подобраны, но для работы достаточно параметра weathercode
                "daily=weathercode,temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours"
            };

            string request = Endpoint + "?" + String.Join("&", query);


            Debug.WriteLine(request);

            var response = await this._httpClient.GetAsync(request);

            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WeatherResponse>(body);

        }



        //Данные классы необходимы для извлечения данных из ответа Open-Meteo.
        //У другого поставщика погодной информации схема JSON будет другой
        public partial class WeatherResponse
        {
            [JsonProperty("latitude")]
            public decimal Latitude { get; set; }

            [JsonProperty("longitude")]
            public decimal Longitude { get; set; }

            [JsonProperty("generationtime_ms")]
            public decimal GenerationtimeMs { get; set; }

            [JsonProperty("utc_offset_seconds")]
            public long UtcOffsetSeconds { get; set; }

            [JsonProperty("timezone")]
            public string Timezone { get; set; }

            [JsonProperty("timezone_abbreviation")]
            public string TimezoneAbbreviation { get; set; }

            [JsonProperty("elevation")]
            public long Elevation { get; set; }

            [JsonProperty("daily_units")]
            public DailyUnits DailyUnits { get; set; }

            [JsonProperty("daily")]
            public Daily Daily { get; set; }
        }

        public partial class Daily
        {
            [JsonProperty("time")]
            public DateTimeOffset[] Time { get; set; }

            [JsonProperty("weathercode")]
            public long[] Weathercode { get; set; }

            [JsonProperty("temperature_2m_max")]
            public decimal[] Temperature2MMax { get; set; }

            [JsonProperty("temperature_2m_min")]
            public decimal[] Temperature2MMin { get; set; }

            [JsonProperty("precipitation_sum")]
            public decimal[] PrecipitationSum { get; set; }

            [JsonProperty("rain_sum")]
            public long[] RainSum { get; set; }

            [JsonProperty("showers_sum")]
            public decimal[] ShowersSum { get; set; }

            [JsonProperty("snowfall_sum")]
            public long[] SnowfallSum { get; set; }

            [JsonProperty("precipitation_hours")]
            public long[] PrecipitationHours { get; set; }
        }

        public partial class DailyUnits
        {
            [JsonProperty("time")]
            public string Time { get; set; }

            [JsonProperty("weathercode")]
            public string Weathercode { get; set; }

            [JsonProperty("temperature_2m_max")]
            public string Temperature2MMax { get; set; }

            [JsonProperty("temperature_2m_min")]
            public string Temperature2MMin { get; set; }

            [JsonProperty("precipitation_sum")]
            public string PrecipitationSum { get; set; }

            [JsonProperty("rain_sum")]
            public string RainSum { get; set; }

            [JsonProperty("showers_sum")]
            public string ShowersSum { get; set; }

            [JsonProperty("snowfall_sum")]
            public string SnowfallSum { get; set; }

            [JsonProperty("precipitation_hours")]
            public string PrecipitationHours { get; set; }
        }
    }
#pragma warning restore CS8618

}
