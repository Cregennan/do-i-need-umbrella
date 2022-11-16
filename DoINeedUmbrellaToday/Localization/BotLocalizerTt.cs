namespace DoINeedUmbrellaToday.Localization
{
    /// <summary>
    /// Вариант локализатора бота для Татарского языка
    /// </summary>
    public class BotLocalizerTt : BotLocalizer
    {
        protected override Dictionary<string, string> Library { get; } = new Dictionary<string, string>()
        {
            {"weather:1", "Аязучан болытлы" },
            {"weather:2 ", "Болытлы"},
            {"weather:3", "Cүрән"},
            {"weather:45" , "Томанлы"},
            {"weather:48" , "Бәс"},
            {"weather:51" , "Сибәләү"},
            {"weather:53" , "Уртача сибәләү"},
            {"weather:55" , "Көчле сибәләү"},
            {"weather:56" , "Салкын сибәләү"},
            {"weather:57" , "Сибәләү"},
            {"weather:61" , "Яңгыр"},
            {"weather:63" , "Уртача яңгыр"},
            {"weather:65" , "Көчле яңгыр"},
            {"weather:66" , "Салкын яңгыр"},
            {"weather:67" , "Көчле салкын яңгыр"},
            {"weather:71" , "Кар"},
            {"weather:73" , "Уртача кар"},
            {"weather:75" , "Көчле кар"},
            {"weather:77" , "Кар"},
            {"weather:80" , "Койма яңгыр"},
            {"weather:81" , "Уртача койма яңгыр"},
            {"weather:82" , "Көчле койма яңгыр"},
            {"weather:85" , "Кар яву"},
            {"weather:86" , "Көчле кар яву"},
            {"weather:95" , "Давыл"},
            {"weather:96" , "Бозлык"},
            {"weather:99" , "Көчле бозлык"},
            {"weather:0", "Аяз" },
            {"langSelected", "Хәзердән башлам сезнең белән татарча сөйләшәчәкбез! Шәһәрегезнең исемен языгыз"},
            {"welcome", "Сәлам, {0}! Телне сайлагыз"},
            {"citySelected", "Әйбәт! Без сезнең яшәгән урыныгызны белдек. Безгә \"Һава торышы\" дип языгыз" },
            {"cityNotFound", "Гафу итегез, без шәһәрегезне таба алмадык. Шәһәр исемен икенчерәк итеп язып карагыз"},
            {"cityRequest", "Шәһәрегезнең исемен языгыз" },
            {"cityBad", "Бу кала дөньяда бармы? Яңыдан язып карагыз"},
            {"cityFound", "Калагызны таптык! Ул {0}, {2} {1} төбәгендә ме?" },
            {"weatherTemplate", "Һава торышы"},
            {"theWeather", "Бүген *{0}* шәһәрендә: \r\n {1}{2} \n {3}! \r\n Бүгенге температура *{4}* һәм *{5}* градус арасында булачак" },
            {"umbrellaTrue", "*Чатырыгызны онытмагыз ☔*" },
            {"umbrellaFalse", "*Чатырыгызны алмасагыз да була ☂*" },
        };

        protected override string FallbackValue => "Cүзләрне таба алмыйм";
    }
}
