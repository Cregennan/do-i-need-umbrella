namespace DoINeedUmbrellaToday.Localization
{
    /// <summary>
    /// Вариант локализатора бота для английского языка
    /// </summary>
    public class BotLocalizerEn : BotLocalizer
    {
        protected override Dictionary<string, string> Library { get; } = new Dictionary<string, string>()
        {
            {"weather:1", "Mainly clear" },
            {"weather:2 ", "Partly cloudy"},
            {"weather:3", "Overcast"},
            {"weather:45" , "Fog"},
            {"weather:48" , "Depositing rime fog"},
            {"weather:51" , "Light drizzle"},
            {"weather:53" , "Moderate drizzle"},
            {"weather:55" , "Dense drizzle"},
            {"weather:56" , "Light freezing drizzle"},
            {"weather:57" , "Light dense drizzle"},
            {"weather:61" , "Slight rain"},
            {"weather:63" , "Moderate rain"},
            {"weather:65" , "Heavy rain"},
            {"weather:66" , "Light feezing rain"},
            {"weather:67" , "Heavy freezing rain"},
            {"weather:71" , "Slight snow"},
            {"weather:73" , "Moderate snow"},
            {"weather:75" , "Heavy snow"},
            {"weather:77" , "Snowy grains"},
            {"weather:80" , "Slight rain showers"},
            {"weather:81" , "Moderate rain showers"},
            {"weather:82" , "Violent rain showers"},
            {"weather:85" , "Slight snow showers"},
            {"weather:86" , "Heavy snow showers"},
            {"weather:95" , "Moderate thunderstorm"},
            {"weather:96" , "Slight hail"},
            {"weather:99" , "Heavy hail"},
            {"weather:0", "Clear sky" },
            {"langSelected", "We will talk to you in English! Type you city name for us to find you on the map"},
            {"welcome", "Welcome, {0}! Select your language"},
            {"citySelected", "Great! Now we now where you live. Send us \"Weather\" and we will tell you it" },
            {"cityNotFound", "Sorry, we don't know this city. Try again and we will find it"},
            {"cityRequest", "Tell us your city and we will find it" },
            {"cityBad", "Does this city really exist? Try again please"},
            {"cityFound", "We found your city! Is it {0} in {1} region of {2}?" },
            {"weatherTemplate", "Weather"},
            {"theWeather", "Today in *{0}*:\r\n {1}{2} \r\n {3}! \r\n The tempetarure will be between *{4}* and *{5}* degrees of celsius." },
            {"umbrellaTrue", "*Don't forget your umbrella ☔*" },
            {"umbrellaFalse", "*You can leave umbrella home ☂*" },
        };


        protected override string FallbackValue { get; } = "String not found";
    }
}
