namespace Chapter4

module Exercise04_09 =
    
    open Houses
    
    let groupedByPrice =
        getHouses 20
        |> Array.groupBy (fun h -> priceBand h.Price)
        |> Array.choose (fun (priceBand, houses) ->
            match houses with
            | [||] -> None
            | _ -> Some (priceBand, houses))
        |> Array.map (fun (priceBand, houses) ->
            let sortedHouses = houses |> Array.sortBy (fun h -> h.Price)
            (priceBand, sortedHouses))
