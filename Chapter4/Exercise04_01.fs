namespace Chapter4

module Exercise04_01 = 

    open Houses

    let housePrices =
        getHouses 20
        |> Array.map (fun h -> sprintf "Address: %s - Price: %f" h.Address h.Price)

