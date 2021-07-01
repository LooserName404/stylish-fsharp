open BillingDetails

[<EntryPoint>]
let main argv =
    let myOrder =
        {
            name = "Kit Eason"
            billing = "112 Fibonacci Street\nErehwon\n35813"
            delivery = AsBilling
        }

    let hisOrder =
        {
            name = "John Doe"
            billing = "314 Pi Avenue\nErehwon\n15926"
            delivery = Physical "16 Planck Parkway\nErehwon\n62291"
        }

    let herOrder =
        {
            name = "Jane Smith"
            billing = "9 Gravity Road\nErehwon\n80665"
            delivery = Download
        }
    
    [ myOrder; hisOrder; herOrder ]
    |> deliveryLabels
    |> printfn "%A"
    
    0
