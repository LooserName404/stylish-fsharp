namespace Chapter11

module MapError =
    
    type Outcome<'TSuccess, 'TFailure> =
        | Success of 'TSuccess
        | Failure of 'TFailure

    let adapt func input =
        match input with
        | Success x -> func x
        | Failure f -> Failure f
    
    let passThrough func input =
        match input with
        | Success x -> func x |> Success
        | Failure f -> Failure f

    let passThroughRejects func input =
        match input with
        | Success x -> Success x
        | Failure f -> func f |> Failure