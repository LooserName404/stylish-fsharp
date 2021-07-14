module Log =
    open System
    open System.Threading

    let report =
        let lockObj = obj()
        fun (color: ConsoleColor) (message: string) ->
            lock lockObj (fun _ -> 
                Console.ForegroundColor <- color
                printfn "%s (thread ID: %i)" message Thread.CurrentThread.ManagedThreadId
                Console.ResetColor())

    let red = report ConsoleColor.Red
    let green = report ConsoleColor.Green
    let yellow = report ConsoleColor.Yellow
    let cyan = report ConsoleColor.Cyan

module Outcome =
    type Outcome =
        | Ok of filename: string
        | Failed of filename: string

    let isOk =
        function
        | Ok _ -> true
        | Failed _ -> false

    let fileName =
        function
        | Ok fn
        | Failed fn -> fn

module Download =
    open System
    open System.IO
    open System.Net
    open System.Text.RegularExpressions

    open FSharp.Data
    open FSharpx.Control

    let private absoluteUri (pageUri: Uri) (filePath: string) =
        if
            filePath.StartsWith("http:")
            || filePath.StartsWith("https:")
        then
            Uri(filePath)
        else
            let sep = '/'

            filePath.TrimStart(sep)
            |> (sprintf "%O%c%s" pageUri sep)
            |> Uri

    let private getLinks (pageUri: Uri) (filePattern: string) =
        async {
            Log.cyan "Getting names..."
            let re = Regex(filePattern)
            let! html = HtmlDocument.AsyncLoad(pageUri.AbsoluteUri)

            let links =
                html.Descendants [ "a" ]
                |> Seq.choose
                    (fun node ->
                        node.TryGetAttribute("href")
                        |> Option.map (fun att -> att.Value()))
                |> Seq.filter (re.IsMatch)
                |> Seq.map (absoluteUri pageUri)
                |> Seq.distinct
                |> Array.ofSeq

            return links
        }

    let private tryDownload (localPath: string) (fileUri: Uri) =
        async {
            let fileName = fileUri.Segments |> Array.last
            Log.yellow (sprintf "%s - starting download" fileName)
            let filePath = Path.Combine(localPath, fileName)

            use client = new WebClient()

            try
                do!
                    client.DownloadFileTaskAsync(fileUri, filePath)
                    |> Async.AwaitTask

                Log.green (sprintf "%s - download complete" fileName)
                return (Outcome.Ok fileName)
            with
            | e ->
                Log.red (sprintf "%s - error: %s" fileName e.Message)
                return (Outcome.Failed fileName)
        }

    let AsyncGetFiles (pageUri: Uri) (filePattern: string) (localPath: string) =
        async {
            let! links = getLinks pageUri filePattern

            let! downloadResults =
                links
                |> Seq.map (tryDownload localPath)
                |> Async.Parallel

            let downloaded, failed =
                downloadResults
                |> Array.partition Outcome.isOk

            return downloaded |> Array.map Outcome.fileName, failed |> Array.map Outcome.fileName
        }
   
    let AsyncGetFilesBatched (pageUri: Uri) (filePattern: string) (localPath: string) (batchSize: int) =
        async {
            let! links = getLinks pageUri filePattern
            let downloaded, failed =
                links
                |> Seq.map (tryDownload localPath)
                |> Seq.chunkBySize batchSize
                |> Seq.collect (fun batch ->
                    batch
                    |> Async.Parallel
                    |> Async.RunSynchronously)
                |> Array.ofSeq
                |> Array.partition Outcome.isOk
            
            return downloaded |> Array.map Outcome.fileName, failed |> Array.map Outcome.fileName
        }
    
    let AsyncGetFilesThrottled (pageUri: Uri) (filePattern: string) (localPath: string) (throttle: int) =
        async {
            let! links = getLinks pageUri filePattern
            
            let! downloadResults =
                links
                |> Seq.map (tryDownload localPath)
                |> Async.ParallelWithThrottle throttle

            let downloaded, failed =
                downloadResults
                |> Array.partition Outcome.isOk
            
            return downloaded |> Array.map Outcome.fileName, failed |> Array.map Outcome.fileName
        }

open System
open System.Diagnostics

[<EntryPoint>]
let main argv =
    let uri =
        Uri @"https://minorplanetcenter.net/data"

    let pattern = @"neam.*\.json\.gz$"

    let localPath = @"D:\projects\stylish-fsharp\Chapter10\downloads"

    let sw = Stopwatch()
    sw.Start()

    let downloaded, failed =
        Download.AsyncGetFilesThrottled uri pattern localPath 4
        |> Async.RunSynchronously

    failed
    |> Array.iter (fun fn -> Log.report ConsoleColor.Red (sprintf "Failed: %s" fn))

    Log.cyan (
        sprintf
            "%i files downloaded in %0.1fs, %i faild. Press a key"
            downloaded.Length
            sw.Elapsed.TotalSeconds
            failed.Length
    )

    Console.ReadKey() |> ignore

    0
