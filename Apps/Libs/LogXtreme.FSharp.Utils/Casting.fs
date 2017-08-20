module LogXtreme.FSharp.Utils.Casting

// https://stackoverflow.com/questions/2361851/c-sharp-and-f-casting-specifically-the-as-keyword
// https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/unchecked.defaultof%5B't%5D-type-function-%5Bfsharp%5D
// https://stackoverflow.com/questions/13520720/unchecked-defaultof-on-a-list
// Usage Example:
// let res = castAs<Type1>(inputValue)
// res is cast to Type1 if 
let castAs<'T when 'T:  null> (o: obj) = 
    match o with 
    | :? 'T as res -> res
    | _ -> null

// https://stackoverflow.com/questions/3151099/is-there-a-way-in-f-to-type-test-against-a-generic-type-without-specifying-the
