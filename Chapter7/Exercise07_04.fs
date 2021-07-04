namespace Chapter7

module Exercise07_04 =
    open System

    [<Struct>]
    type Position =
        { X: float32
          Y: float32
          Z: float32
          Time: DateTime }
    
    let translate x y z position =
        { position with 
            X = position.X + x
            Y = position.Y + y
            Z = position.Z + z}
