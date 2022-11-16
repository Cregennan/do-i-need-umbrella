using Microsoft.Extensions.Localization;

namespace DoINeedUmbrellaToday.Localization
{
    /// <summary>
    /// Сервис предоставляет перевод фраз бота на нужные языки
    /// </summary>
    public abstract class BotLocalizer : IStringLocalizer
    {
        /// <summary>
        /// Возвращает локализованный вариант строки по ее уникальному коду
        /// </summary>
        /// <param name="name">Уникальный код</param>
        /// <returns>Строка на нужном языке</returns>
        public LocalizedString this[string name]
        {
            get
            {

                if (this.Library.ContainsKey(name))
                {
                    return new LocalizedString(name, this.Library[name]);
                }
                else
                {
                    return new LocalizedString(name, this.FallbackValue);
                }

            }
        }

        /// <summary>
        /// Возвращает локализованный вариант строки по ее уникальному коду
        /// </summary>
        /// <param name="name">Уникальный код</param>
        /// <returns>Строка на нужном языке</returns>
        public LocalizedString this[string name, params object[] arguments]
        {

            get
            {
                return this[name];
            }
        }

        /// <summary>
        /// Язык для локализации по умолчанию
        /// </summary>
        protected static BotLocalizer FallbackLocalizer = new BotLocalizerRu();

        /// <summary>
        /// Все пары ключ-значение текущего языка
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <returns></returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return Library.Select(x => new LocalizedString(x.Key, x.Value));
        }

        /// <summary>
        /// Коллекция данных о переводе на нужный язык
        /// </summary>
        protected abstract Dictionary<String, String> Library { get; }

        /// <summary>
        /// Значение при отсутствии нужного ключа в базе перевода
        /// </summary>
        protected abstract String FallbackValue { get; }

        /// <summary>
        /// Возвращает реализацию BotLocalizer для выбранного языка
        /// </summary>
        /// <param name="lang">Язык, поддерживаемые языки: en, ru, tt</param>
        /// <returns></returns>
        public static BotLocalizer GetCurrentLocalizer(String lang)
        {
            return lang switch
            {
                "en" => new BotLocalizerEn(),
                "tt" => new BotLocalizerTt(),
                _ => new BotLocalizerRu(),
            };
        }



    }
}
