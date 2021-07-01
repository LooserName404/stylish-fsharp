namespace Chapter4

module Exercise04_03 =
    
    open Houses
    
    let expensiveHouses =
        getHouses 20
        |> Array.filter (fun h -> h.Price > 250_000m)

