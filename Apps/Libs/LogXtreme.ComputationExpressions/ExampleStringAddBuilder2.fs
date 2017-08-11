module public LogXtreme.ComputationExpressions.Examples.StringAddBuilder2

// let's add some tracing to see how it works in the F# interactive window

let strToInt str = System.Int32.TryParse str


type TestOpBuilder()=  

    member this.Bind(m, f) = 
        match m with 
        | true, i -> 
            printfn "Continue with %A on %A" f i 
            f i 
        | false, _ -> 
            printfn "None: stop continuation" 
            None

    member this.Return(x) = 
        printfn "Returning Some of %A" x
        Some(x)

let testOp = TestOpBuilder()

let stringAddWorkflow x y z = 
    testOp
        {
        let! a = strToInt x 
        let! b = strToInt y 
        let! c = strToInt z 
        return a + b + c    
        }

// test
let good = stringAddWorkflow "12" "3" "2"
let bad = stringAddWorkflow "12" "xyz" "2"