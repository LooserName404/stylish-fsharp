namespace Chapter5

module Clip =

    let inline clip (ceiling : 'T) (s : 'T seq) =
        s
        |> Seq.map (fun i -> min i ceiling)
    
    let res =
        [| 1.0; 2.3; 10.0; -5.0 |]
        |> clip 10.

    printfn "%A" res
