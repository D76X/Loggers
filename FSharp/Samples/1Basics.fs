let mutable a = 10
a <- 20

let items = [1..5]
let itemsPlusOne = List.append items [6]
items

let prefix prefixStr baseStr = prefixStr+","+baseStr

let myPrefix = prefix "Hello"

let names = ["one"; "two"; "three"]

let bang stringVal = stringVal+"!" 

let modifiedNames = names
                    |>Seq.map(myPrefix)
                    |>Seq.map(bang)
                    |>Seq.toList

