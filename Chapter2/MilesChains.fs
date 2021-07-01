module MilesChains

open System

let private (~~) = float

type MilesChains = private | MilesChains of wholeMiles : int * chains : int

let fromMilesPointChains(milesPointChains : float) : MilesChains =
    if milesPointChains < 0. then
        raise <| ArgumentOutOfRangeException("", "")
    
    let wholeMiles = milesPointChains |> floor |> int
    let fraction = milesPointChains - ~~wholeMiles
    
    if fraction > 0.79 then
        raise <| ArgumentOutOfRangeException("", "")
    
    let chains = fraction * 100. |> round |> int
    MilesChains(wholeMiles, chains)
    
let toDecimalMiles (MilesChains(wholeMiles, chains)) : float =
    ~~wholeMiles + (~~chains / 80.)
