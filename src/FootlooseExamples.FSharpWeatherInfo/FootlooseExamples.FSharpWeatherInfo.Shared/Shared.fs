namespace FootlooseExamples.FSharpWeatherInfo.Shared

open System

type WeatherInfoRequest = {
    City : string }

type WeatherInfoResponse = {
    City : string 
    Temperature :string }

type IWeatherInfoService =
    abstract HandleRequest: WeatherInfoRequest -> WeatherInfoResponse