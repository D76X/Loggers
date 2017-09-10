module LogXtreme.FSharp.UtilsFunctions

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

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


// Quotation to read the property name...
// http://www.contactandcoil.com/software/dotnet/getting-a-property-name-as-a-string-in-f/
let propertyName quotation = 
    match quotation with 
    | PropertyGet (_,propertyInfo,_) -> propertyInfo.Name
    | _ -> System.String.Empty
                
       
