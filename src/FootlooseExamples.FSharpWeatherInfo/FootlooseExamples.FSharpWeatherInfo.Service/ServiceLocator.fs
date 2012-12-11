namespace FootlooseExamples.FSharpWeatherInfo.Service

open FootlooseExamples.FSharpWeatherInfo.Shared
open System

type ServiceLocatorDummy() =
    interface IServiceProvider with
        member this.GetService (serviceType:Type) : obj = failwith "Not implemented"

    interface Microsoft.Practices.ServiceLocation.IServiceLocator with
        member this.GetInstance (serviceType:Type) : obj =
            match serviceType with
            | x when x = typeof<IWeatherInfoService> -> new WeatherInfoService() :> obj
            | _ -> failwith ("ServiceLocator is unable to find implementation of " + serviceType.FullName)

        member this.GetInstance(serviceType:Type,key:string) = failwith "Not implemented"
        member this.GetInstance<'a>(key:string) :'a = failwith "Not implemented"
        member this.GetInstance<'a>() :'a = failwith "Not implemented"
        member this.GetAllInstances(serviceType:Type) = failwith "Not implemented"
        member this.GetAllInstances<'a>() :'a seq = failwith "Not implemented"