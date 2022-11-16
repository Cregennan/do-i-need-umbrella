namespace DoINeedUmbrellaToday.Exceptions
{

    /// <summary>
    /// Служебное исключение, нужно для удобного ответа бота
    /// </summary>
    public class TelegramMessageResultException : TelegramException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dropMarkup"></param>
        public TelegramMessageResultException(String message, bool dropMarkup = false) : base(message)
        {
            this.DropMarkup = dropMarkup;
        }

        /// <summary>
        /// Поле указывает на то, нужно ли сбросить текущие кнопки пользователя
        /// </summary>
        public bool DropMarkup { get; }
    }
}
