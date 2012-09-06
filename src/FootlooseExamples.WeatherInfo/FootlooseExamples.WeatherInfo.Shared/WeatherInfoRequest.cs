using System;

namespace FootlooseExamples.WeatherInfo.Shared
{
    [Serializable]
    public class WeatherInfoRequest
    {
        public string City { get; set; }
    }
}