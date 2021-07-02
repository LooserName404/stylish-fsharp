namespace Chapter4

module Exercise04_10 =

    open Houses

    let inline tryAverageBy (operation : 'T -> 'U) (collection : 'T seq) =
        match collection with
        | collection when Seq.isEmpty collection -> None
        | _ -> collection |> Seq.averageBy operation |> Some

    let avg =
        getHouses 20
        |> Array.filter (fun h -> h.Price > 200_000m)
        |> tryAverageBy (fun h -> h.Price)
        |> Option.defaultValue 0m
