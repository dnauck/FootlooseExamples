open System

open FootlooseExamples.FSharpWeatherInfo.Service
open FootlooseExamples.FSharpWeatherInfo.Shared
open Footloose

[<EntryPoint>]
let main argv =
    use connection = 
        Fluently.Configure()
            .UseSerializerOfType<Serialization.BinarySerializer>()
            .UseServiceLocator(new ServiceLocatorDummy())
            .WithServiceContracts(fun contracts -> contracts.WithServiceContract.RegisterOfType<IWeatherInfoService>() |> ignore)
            .UseTransportChannel(Configuration.Fluent.IpcTransportChannelConfiguration.Standard)
            .WithEndpointIdentifier("footloose-weatherinfoservice")
            .CreateConnection(new IO.FileInfo("Footloose.lic"))
    
    connection.ExceptionOccurred.Add (fun eventArgs -> printf "Exception occurred: %A" eventArgs.Exception)

    connection.Open()
 
    printfn "Footloose Connection is now listening on: %A" connection.EndpointIdentityManager.SelfEndpointIdentity.LocalEndpoint.Uri
 
    printfn "Press ENTER to exit..."
    Console.ReadLine() |> ignore
 
    connection.Close()

    0