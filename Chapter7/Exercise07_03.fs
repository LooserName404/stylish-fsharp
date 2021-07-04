namespace Chapter7

module Exercise07_03 =
    type Track = { Name: string; Artist: string }

    let tracks =
        [ { Name = "The Mollusk"
            Artist = "Ween" }
          { Name = "Bread Hair"
            Artist = "They Might Be Giants" }
          { Name = "The Mollusk"
            Artist = "Ween" } ]
        |> Set.ofList
