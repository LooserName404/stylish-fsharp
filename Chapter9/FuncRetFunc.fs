namespace Chapter9

module FuncRetFunc =
    
    let counter start =
        let mutable current = start
        fun () ->
            let this = current
            current <- current + 1
            this
    
    let rangeCounter start finish =
        let mutable current = start
        fun () ->
            let this = current
            if current + 1 > finish then
                current <- start
            else
                current <- current + 1
            this