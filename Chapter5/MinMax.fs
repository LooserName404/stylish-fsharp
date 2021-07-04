namespace Chapter5

module MinMax =
    let minMax (s : 'T seq) =
        s |> Seq.min, s |> Seq.max
