module LogXtreme.FSharp.Test.Casting

// https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/casting-and-conversions
// https://fsharpforfunandprofit.com/posts/inheritance/

open System
open System.Numerics
open Xunit
open Swensen.Unquote

type BaseGeometry()=
    abstract member Area: unit -> float
    abstract member MajorLength: unit -> float
    abstract member MinorLength: unit -> float
    default this.Area() = float 0
    default this.MajorLength() = float 0
    default this.MinorLength() = float 0
    static member Pi = System.Math.PI

type Point2D(x,y)=
    inherit BaseGeometry()
    member this.X: float = x
    member this.Y: float = y

// https://stackoverflow.com/questions/7778228/how-to-choose-between-private-member-and-let-binding
// https://stackoverflow.com/questions/18138870/whats-the-difference-in-f-between-a-private-member-and-a-let-val?noredirect=1&lq=1
// https://stackoverflow.com/questions/24840948/when-should-i-use-let-member-val-and-member-this
type Line2D(p1: Point2D, p2: Point2D)=
    inherit BaseGeometry()    
    let dx2 = p1.X*p1.X + p2.X*p2.X-p1.X*p2.X*2. 
    let dy2 = p1.Y*p1.Y + p2.Y*p2.Y-p1.Y*p2.Y*2.  
    let d = sqrt dx2 + dy2
    member this.P1 = p1
    member this.P2 = p2    
    override this.MajorLength() = d        
    override this.MinorLength() = d

type Circle2D(o: Point2D, p: Point2D)=
    inherit BaseGeometry()
    let r2x = o.X*o.X+p.X*p.X-o.X*p.X*2.
    let r2y = o.Y*o.Y+p.Y*p.Y-o.Y*p.Y*2.
    let r2 = r2x + r2y
    let r = sqrt r2
    let d = 2.*r
    let perimeter = BaseGeometry.Pi * d  
    let area = BaseGeometry.Pi * r2    
    member this.Centre = o
    member this.Border = p  
    member this.Radius = r
    member this.Diameter = d
    member this.Perimeter = perimeter
    override this.MajorLength() = this.Perimeter        
    override this.MinorLength() = this.Radius
    override this.Area() = area




