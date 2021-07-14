namespace Chapter11

module Pipeline =

    open System

    type Message = { FileName: string; Content: float [] }

    type Reading =
        { TimeStamp: DateTimeOffset
          Data: float [] }

    let example =
        [| { FileName = "2019-02-23T02:00:00-05:00"
             Content = [| 1.0; 2.0; 3.0; 4.0 |] }
           { FileName = "2019-02-23T02:00:10-05:00"
             Content = [| 5.0; 6.0; 7.0; 8.0 |] }
           { FileName = "error"; Content = [||] }
           { FileName = "2019-02-23T02:00:20-05:00"
             Content = [| 1.0; 2.0; 3.0; Double.NaN |] } |]

    let log s = printfn "Logging: %s" s

    type MessageError =
        | InvalidFileName of fileName: string
        | DataContainsNaN of fileName: string * index: int
    
    let getReading message =
        match DateTimeOffset.TryParse(message.FileName) with
        | true, dt ->
            let reading = { TimeStamp = dt; Data = message.Content }
            Ok (message.FileName, reading)
        | false, _ ->
            Error (InvalidFileName message.FileName)

    let validateData(fileName, reading) =
        let nanIndex =
            reading.Data
            |> Array.tryFindIndex (Double.IsNaN)
        match nanIndex with
        | Some i ->
            Error (DataContainsNaN (fileName, i))
        | None ->
            Ok reading

    let logError (e: MessageError) =
        match e with
        | InvalidFileName fileName -> log $"Invalid file name: {fileName}"
        | DataContainsNaN (fileName, index) -> log $"Data contains NaN: {fileName} - Index {index}"

    
