using System;

namespace FootlooseExamples.WeatherInfo.Shared
{
    [Serializable]
    public class WeatherInfoResponse
    {
        public string City { get; set; }
        public string Temperature { get; set; }
    }
}