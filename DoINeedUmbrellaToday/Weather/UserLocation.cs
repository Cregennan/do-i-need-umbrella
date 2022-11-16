using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoINeedUmbrellaToday.Weather
{
    /// <summary>
    /// Класс, описывающий местоположение пользователя
    /// </summary>
    public class UserLocation
    {

        /// <summary>
        /// Широта города пользователя
        /// </summary>
        [Range(-180, 180, ErrorMessage = "Latitude should be in range [-180, 180]")]
        [BindRequired]
        [BindProperty(Name = "lat", SupportsGet = true)]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Долгота города пользователя
        /// </summary>
        [Range(-90, 90, ErrorMessage = "Longitude should be in range [-90, 90]")]
        [BindRequired]
        [BindProperty(Name = "long", SupportsGet = true)]
        public decimal? Longitude { get; set; }

        
        /// <summary>
        /// Возвращает true если местоположение установлено верно и false в противном случае
        /// </summary>
        [JsonIgnore]
        internal bool Validated
        {
            get
            {
                return Latitude <= 180 && Latitude >= -180 && Longitude >= -90 && Longitude <= 90;
            }
        }


        //Производный от UserLocation класс с более подробным описанием местоположения пользователя
        public class Result : UserLocation
        {
            //Название города пользователя
            public String? CityName { get; set; }

            //Название региона страны пользователя
            public String? RegionName { get; set; }

            //Название страны пользователя
            public String? CountryName { get; set; }
        }




    }
}
