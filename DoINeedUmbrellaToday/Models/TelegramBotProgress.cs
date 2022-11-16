namespace DoINeedUmbrellaToday.Models
{

    /// <summary>
    /// Модель описывает прогресс текущего пользователя по боту
    /// </summary>
    public class TelegramBotProgress
    {
        /// <summary>
        /// Идентификатор чата с пользователем
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название города пользователя
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Широта города
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Долгота города
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Последний запрос пользователя на поиск города
        /// </summary>
        public string? LastQuery { get; set; }

    }
}
