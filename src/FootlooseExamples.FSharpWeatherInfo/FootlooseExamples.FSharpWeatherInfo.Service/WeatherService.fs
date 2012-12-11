namespace FootlooseExamples.FSharpWeatherInfo.Service

open FootlooseExamples.FSharpWeatherInfo.Shared
open System

type WeatherInfoService() =
    interface IWeatherInfoService with
        member this.HandleRequest request = { City = request.City; Temperature = "25° C" }