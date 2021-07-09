namespace Chapter8
open System

module GreyScale =

    open System.Drawing

    type GreyScale(r: byte, g: byte, b: byte) =

        new (color: Color) =
            GreyScale(color.R, color.G, color.B)

        member __.Level = (int r + int g + int b) / 3 |> byte

        override this.ToString() =
            sprintf "Greyscale(%i)" this.Level
        
        override this.Equals(that) =
            match that with
            | :? GreyScale as g -> g.Level = this.Level
            | _ -> false
        
        override __.GetHashCode() = hash (r, g, b)

        interface IEquatable<GreyScale> with
            member this.Equals(that) = that.Level = this.Level