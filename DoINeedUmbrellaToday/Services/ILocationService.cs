using DoINeedUmbrellaToday.Weather;

namespace DoINeedUmbrellaToday.Services
{
    /// <summary>
    /// Сервис предоставляет данные о местоположении пользователя по запросу с названием города
    /// </summary>
    public interface ILocationService
    {

        /// <summary>
        /// Метод, возвращающий данные о городе по его названию
        /// </summary>
        /// <param name="q">Название города</param>
        /// <returns>Данные о координатах города, его названии, страны и регионе</returns>
        public Task<UserLocation.Result> GetLocationAsync(string q);

    }
}
