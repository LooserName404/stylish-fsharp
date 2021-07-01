namespace Chapter4

module Exercise04_07 =
    
    open Houses
    
    let expensiveAverage =
        getHouses 20
        |> Array.filter (fun h -> h.Price > 200_000m)
        |> Array.averageBy (fun h -> h.Price)

