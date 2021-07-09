namespace Chapter9

module Partial =

    let featureScale a b xMin xMax x =
        a + ((x - xMin) * (b - a)) / (xMax - xMin)

    let scale (data: float seq) =
        let minX = data |> Seq.min
        let maxX = data |> Seq.max

        let zeroOneScale =
            featureScale 0. 1. minX maxX

        data
        |> Seq.map zeroOneScale
        