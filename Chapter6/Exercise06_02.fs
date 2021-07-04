namespace Chapter6

module Exercise06_02 =

    type FruitBatch = { Name: string; Count: int }

    let fruits =
        [ { Name = "Apples"; Count = 3 }
          { Name = "Oranges"; Count = 4 }
          { Name = "Bananas"; Count = 2 } ]
    
    for {Name = name; Count = count} in fruits do
        printfn "There are %i %s" count name
    
    fruits
    |> List.iter (fun {Name = name; Count = count} ->
        printfn "There are %i %s" count name)
