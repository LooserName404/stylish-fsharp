module BillingDetails

type Delivery =
    | AsBilling
    | Physical of string
    | Download
    | ClickAndCollect of int

type BillingDetails =
    {
        name: string
        billing: string
        delivery: Delivery
    }

let tryDeliveryLabel (billingDetails: BillingDetails) =
    match billingDetails.delivery with
    | AsBilling -> billingDetails.billing |> Some
    | Physical address -> address |> Some
    | Download -> None
    | ClickAndCollect _ -> None
    |> Option.map (fun address -> sprintf "%s\n%s" billingDetails.name address)

let deliveryLabels (billingDetails: BillingDetails seq) =
    billingDetails |> Seq.choose tryDeliveryLabel

let collectionsFor (storeId : int) (billingDetails : BillingDetails seq) =
    billingDetails
    |> Seq.choose (fun d ->
        match d.delivery with
        | ClickAndCollect s when s = storeId -> Some d
        | _ -> None)
