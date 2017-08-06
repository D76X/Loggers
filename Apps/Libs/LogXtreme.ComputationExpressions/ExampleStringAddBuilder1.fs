module public LogXtreme.ComputationExpressions.Examples.StringAddBuilder1

// given a string in F# we can try to parse it using the .NET standard 
// function but F# spares us the need to use the "out" parameter which
// would be necessary in C#
let strToInt str = System.Int32.TryParse str

// in the Bind given a m to be of type tuple (bool, 'a) test this value against the 
// pattern (true, i) and if it is a match then pass the second term of the tuple i to 
// the continuation f. Clearly, f must be an expression that takes the type of i, thus 
// this type will be the integer type. If the first m or any (f i) evaluates to the a 
// value matching the pattern (false,_) instead then continue with None which signal Bind 
// to stop continuation until the end of the computation is reached.
type TestOpBuilder()=  

    member this.Bind(m, f) = 
        match m with 
        | true, i -> f i
        | false, _ -> None

    member this.Return(x) = Some(x)

let testOp = TestOpBuilder()

let stringAddWorkflow x y z = 
    testOp
        {
        let! a = strToInt x // unwrap the evaluation strToInt x into a
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
        }

// test
let good = stringAddWorkflow "12" "3" "2"
let bad = stringAddWorkflow "12" "xyz" "2"

// reads as

//Bind(let (t1,t2) = strToInt x, fun
