namespace FootlooseExamples.WeatherInfo.Shared
{
    public interface IWeatherInfoService
    {
        WeatherInfoResponse HandleRequest(WeatherInfoRequest request);
    }
}