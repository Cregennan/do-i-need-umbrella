
namespace DoINeedUmbrellaToday.Models
{
    /// <summary>
    /// Модель описывает данные пользователя чата Telegram
    /// </summary>
    public class TelegramChat
    {

        /// <summary>
        /// Допустимые языки общения с пользователем
        /// </summary>
        public enum Languages
        {
            Russian,
            English,
            Tatar
        }

        /// <summary>
        /// Идентификатор чата с пользователем
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Выбранный пользователем язык
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Текущее состояние взаимодействия с пользователем
        /// </summary>
        public string State { get; set; }


    }
}
