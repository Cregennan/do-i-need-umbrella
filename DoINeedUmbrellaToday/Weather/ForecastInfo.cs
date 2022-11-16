namespace DoINeedUmbrellaToday.Weather
{
    /// <summary>
    /// Класс, описывающий погодные данные
    /// </summary>
    public class ForecastInfo
    {

        /// <summary>
        /// Код погоды по OpenMeteo
        /// </summary>
        public long WeatherCode { get; set; }

        /// <summary>
        /// Часы осадков в сумме
        /// </summary>
        public decimal PrecipitationHours { get; set; }

        /// <summary>
        /// Часы дождя в сумме
        /// </summary>
        public decimal RainSum { get; set; }

        /// <summary>
        /// Часы ливня в сумме
        /// </summary>
        public decimal ShowersSum { get; set; }

        /// <summary>
        /// Максимальная температура за день
        /// </summary>
        public decimal TemperatureMax { get; set; }

        /// <summary>
        /// Минимальная температура за день
        /// </summary>
        public decimal TemperatureMin { get; set; }

        /// <summary>
        /// Иконки различных погодных явлений
        /// </summary>
        public string WeatherCodeIcons
        {
            get
            {
                return this.WeatherCode switch
                {
                    1 => "🌤",
                    2 => "⛅",
                    3 => "☁",
                    45 => "🌫",
                    48 => "🌫",
                    51 => "🌧",
                    53 => "🌧",
                    55 => "🌧",
                    56 => "🌧",
                    57 => "🌧",
                    61 => "🌦",
                    63 => "🌧",
                    65 => "💧",
                    66 => "💧💧",
                    67 => "💧💧💧",
                    71 => "❄",
                    73 => "❄❄",
                    75 => "☃❄❄",
                    77 => "🌨",
                    80 => "🌧🌧",
                    81 => "🌧🌧🌧",
                    82 => "🌨",
                    85 => "🌨🌨",
                    86 => "☃🌨🌨",
                    95 => "⛈🌩",
                    96 => "🌪",
                    99 => "🌪",
                    _ => "☼",
                };
            }
        }

        /// <summary>
        /// Нужен ли зонт
        /// </summary>
        public bool UmbrellaRequired
        {
            get
            {
                switch (this.WeatherCode)
                {
                    case 53:
                    case 55:
                    case 56:
                    case 57:
                    case 61:
                    case 63:
                    case 65:
                    case 66:
                    case 67:
                    case 80:
                    case 81:
                    case 82:
                        return true;
                    default:
                        return false;
                }
            }
        }


    }
}
