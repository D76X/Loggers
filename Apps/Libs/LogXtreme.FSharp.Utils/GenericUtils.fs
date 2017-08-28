module LogXtreme.FSharp.UtilsFunctions

// Efficient concatenation of objects into a string with string builder
// https://stackoverflow.com/questions/18595597/is-using-a-stringbuilder-a-right-thing-to-do-in-f
let strconc = 
    fun (data) ->         
        let sb = new System.Text.StringBuilder()
        for o in data do sb.Append(o.ToString()) |> ignore
        sb.ToString()

// examples
strconc ["one"; "two"] |> printfn "%s"
strconc [1;2;3] |> printfn "%s"
strconc (["one"; "two"; 3 ]: list<obj>) |> printfn "%s"

                
       
