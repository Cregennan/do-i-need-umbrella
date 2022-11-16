using DoINeedUmbrellaToday.Weather;

namespace DoINeedUmbrellaToday.Services
{
    /// <summary>
    /// Сервис предоставляет данные о погодных условиях в течение дня
    /// </summary>
    public interface IForecastService
    {

        /// <summary>
        /// Метод возвращает прогноз погоды на текущий день
        /// </summary>
        /// <param name="location">Объект с данными о местоположении</param>
        /// <returns>Объект с погодными данными</returns>
        public Task<ForecastInfo> GetForecastForToday(UserLocation location);


    }
}
