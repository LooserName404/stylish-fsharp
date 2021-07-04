namespace Chapter6

module Exercise06_03 =

    open System
    open System.Text.RegularExpressions

    let zipCodes =
        [ "90210"
          "94043"
          "94043-0138"
          "10013"
          "90210-3124"
          "10013" ]

    let (|USZipCode|_|) s =
        let m = Regex.Match(s, @"^(\d{5})$")

        if m.Success then
            USZipCode s |> Some
        else
            None

    let (|USZipPlus4Code|_|) s =
        let m = Regex.Match(s, @"^(\d{5})\-(\d{4})$")

        if m.Success then
            USZipPlus4Code(m.Groups.[1].Value, m.Groups.[2].Value)
            |> Some
        else
            None

    zipCodes
    |> List.iter
        (fun z ->
            match z with
            | USZipCode c -> printfn "A normal zip code: %s" c
            | USZipPlus4Code (code, suffix) -> printfn "A Zip+4 code: prefix %s, suffix %s" code suffix
            | _ as n -> printfn "Not a zip code: %s" n)
