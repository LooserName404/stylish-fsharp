namespace Chapter4

module Exercise04_11 =

    open Houses

    let single =
        getHouses 20
        |> Array.choose (fun h -> 
            match trySchoolDistance h with
            | Some _ -> h |> Some
            | None -> None)
        |> Array.tryFind (fun h -> h.Price < 100_000m)
        