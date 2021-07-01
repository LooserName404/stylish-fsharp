namespace Chapter4

module Exercise04_08 =
    
    open Houses
    
    let houseAndDistance =
        getHouses 20
        |> Array.choose (fun h ->
            match trySchoolDistance h with
            | Some _ -> h |> Some
            | None -> None)
        |> Array.find (fun h -> h.Price > 100_000m) 
