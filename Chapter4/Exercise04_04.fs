namespace Chapter4

module Exercise04_04 =
    
    open Houses
    
    let houseAndDistance =
        getHouses 20
        |> Array.choose (fun h ->
            match trySchoolDistance h with
            | Some x -> (h, x) |> Some
            | None -> None)

