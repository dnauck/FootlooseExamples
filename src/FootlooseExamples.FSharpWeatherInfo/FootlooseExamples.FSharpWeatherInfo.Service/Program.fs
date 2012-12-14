open System

open FootlooseExamples.FSharpWeatherInfo.Service
open FootlooseExamples.FSharpWeatherInfo.Shared
open Footloose

[<EntryPoint>]
let main argv =
    use connection = 
        Fluently.Configure()
            .SerializerOfType<Serialization.BinarySerializer>()
            .ServiceLocator(new ServiceLocatorDummy())
            .ServiceContracts(fun contracts -> contracts.ServiceContract.RegisterOfType<IWeatherInfoService>() |> ignore)
            .TransportChannel(Configuration.Fluent.IpcTransportChannelConfiguration.Standard.EndpointIdentifier("footloose-weatherinfoservice"))
            .CreateConnection(new IO.FileInfo("Footloose.lic"))
    
    connection.ExceptionOccurred.Add (fun eventArgs -> printf "Exception occurred: %A" eventArgs.Exception)

    connection.Open()
 
    printfn "Footloose Connection is now listening on: %A" connection.EndpointIdentityManager.SelfEndpointIdentity.LocalEndpoint.Uri
 
    printfn "Press ENTER to exit..."
    Console.ReadLine() |> ignore
 
    connection.Close()

    0