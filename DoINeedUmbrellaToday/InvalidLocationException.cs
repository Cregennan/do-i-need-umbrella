namespace DoINeedUmbrellaToday
{
    /// <summary>
    /// Exception is thrown when user tries to get weather data for invalid location
    /// </summary>
    public class InvalidLocationException : Exception
    {
        public InvalidLocationException() : base("Provided location does not exist on Earth")
        {

        }


    }
}
