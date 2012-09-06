using FootlooseExamples.WeatherInfo.Shared;

namespace FootlooseExamples.WeatherInfo.Service
{
    public class WeatherInfoService : IWeatherInfoService
    {
        #region Implementation of IWeatherInfoService

        public WeatherInfoResponse HandleRequest(WeatherInfoRequest request)
        {
            return new WeatherInfoResponse()
                {
                    City = request.City,
                    Temperature = "25° C"
                };
        }

        #endregion
    }
}