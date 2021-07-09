namespace Chapter9

module Composition =

    let pipeline =
        [ fun x -> x * 2.
          fun x -> x * x
          fun x -> x - 99.9 ]

    let applyAll (p: (float -> float) list) =
        p |> List.reduce (>>)
