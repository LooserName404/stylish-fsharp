namespace Chapter9

module Args =

    let add a b = a + b

    let applyAndPrint f a b =
        let r = f a b
        printfn "%i" r
    
    let multiply a b = a * b

    applyAndPrint add 2 3
    applyAndPrint multiply 2 3
    applyAndPrint (fun a b -> a - b) 2 3
    applyAndPrint (-) 2 3