namespace Chapter4

module Exercise04_02 =
    
    open Houses
    
    let pricesAverage =
        getHouses 20
        |> Array.averageBy (fun h -> h.Price)

