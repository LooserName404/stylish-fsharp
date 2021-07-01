namespace Chapter4

module Exercise04_06 =
    
    open Houses
    
    getHouses 20
    |> Array.filter (fun h -> h.Price > 250_000m)
    |> Array.sortByDescending (fun h -> h.Price)
    |> Array.iter (fun h -> printfn "Address: %s Price: %f" h.Address h.Price)

