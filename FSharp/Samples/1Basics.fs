let mutable a = 10
a <- 20

let items = [1..5]
let itemsPlusOne = List.append items [6]
items

let prefix prefixStr baseStr = prefixStr+","+baseStr

let myPrefix = prefix "Hello"

let names = ["one"; "two"; "three"]

let bang stringVal = stringVal+"!"

// composition of functions
let composedForward = myPrefix >> bang
let composedBackwards = myPrefix << bang

let modifiedNames1 = names
                    |>Seq.map(myPrefix)
                    |>Seq.map(bang)
                    |>Seq.toList

let modifiedNames2 = names
                    |>Seq.map(composedForward)                  
                    |>Seq.toList

                    
let modifiedNames3 = names
                    |>Seq.map(composedBackwards)                  
                    |>Seq.toList

printfn "print the integer number %d" 100
printfn "print the string %s" "like bang!"
printfn "print the float %f" 10.5

let numbers = [1..5]
let processNumberEx1 =  numbers |> Seq.map(fun x -> printfn "mapped to %d" x; x*x) |> Seq.sort |> Seq.toList

let processNumberEx2 = 
    numbers
    |> Seq.map(fun x -> printfn "mapped to %d" x; x*x*x)
    |> Seq.sort
    |> Seq.toList



