open System
open FootlooseExamples.FSharpWeatherInfo.Shared
open Footloose.Configuration
open FSharpx
open FSharpx.Task
open FSharpx.Linq
open System.Threading
open System.Linq.Expressions
open Footloose
open Footloose.DataModel


let openConnection() =
    let connection = 
        Fluently.Configure()
            .SerializerOfType<Serialization.BinarySerializer>()
            .TransportChannel(Configuration.Fluent.IpcTransportChannelConfiguration.Standard)
            .CreateConnection(new IO.FileInfo("Footloose.lic"))            

    connection.ExceptionOccurred.Add (fun eventArgs -> printf "Exception occurred: %A" eventArgs.Exception)
    connection.Open()
    connection

[<EntryPoint>]
let main argv =

    /// active pattern for easier access of the service result
    let (|Success|Failure|) (response:IMethodResponse<'a>) =    
        match response.Exception with
        | null -> Success(response.ReturnValue)
        | exn ->  Failure(response.Exception)        

    use connection = openConnection()

    let serviceUri = connection.UriBuilder.BuildEndpointUri(Environment.UserName, Environment.MachineName, "footloose-weatherinfoservice")
    let cts = new CancellationTokenSource()

    let task = TaskBuilder(cancellationToken = cts.Token)

    let t() =
        task {  
            printfn "Waiting for incoming result... "
            let! result =
                connection.Invoke(
                    <@ fun (service:IWeatherInfoService) -> service.HandleRequest { City = "Hamburg" } @>
                    |> toLinqExpression,
                    cts.Token,
                    serviceUri)

            return
                match result with // using active pattern
                | Success result -> result
                | Failure exn -> raise exn
        }

    match FSharpx.Task.run t with
    | Task.Canceled -> failwith "Task should have been successful, but was canceled"
    | Task.Error e -> failwith "Task should have been successful, but errored with exception %A" e
    | Task.Successful result -> printfn "%s %s" result.City result.Temperature

    Console.ReadLine() |> ignore
 
    connection.Close()
    0