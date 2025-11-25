// For more information see https://aka.ms/fsharp-console-apps
// <p>Text <img src="dsadsa.png"></p>

// Builder Pattern
//let p args = 
//    let allArgs = args |> String.concat "\n"
//    ["<p>"; allArgs; "</p>"] |> String.concat "\n"

//let img url = "<img src=\"" + url + "\"/>"

//[<EntryPoint>]
//let main argv =
//    let html = 
//        p [
//            "Check out this picture" 
//            img "pokemon.com./pikachu.png"
//        ]

//    printfn "%s" html
//    0


//Decorator Pattern
//open System.Diagnostics

//let doWork() =
//    printf "Doing some work"

//let logger work name =
//    let sw = Stopwatch.StartNew()
//    printfn "%s %s" "Entering function" name
//    work()
//    sw.Stop()
//    printfn "Exiting method %s: %fs elapsed" name sw.Elapsed.TotalSeconds

//[<EntryPoint>]
//let main argv = 
//    let work() = logger doWork "do_work"
//    work()
//    0

// Factory Pattern
//type ICountryInfo =
//    abstract member Capital : string

//type Country =
//    | USA
//    | UK
//with
//    static member Create = function
//        | "USA" | "America" -> USA
//        | "UK" | "England" -> UK
//        | _ -> failwith "No such country"

//let make country = 
//    match country with
//    | USA -> { new ICountryInfo with member x.Capital = "Washington"}
//    | UK -> { new ICountryInfo with member x.Capital = "London"}

//[<EntryPoint>]
//let main argv =
//    let uk = Country.Create "UK"
//    let usa = make Country.USA
//    printfn "%s" usa.Capital
//    0

//Interpreter Pattern
open System
open System.IO
open System.Xml
open System.Xml.Linq
open Microsoft.FSharp.Reflection

type Expression =
  Math of Expression list
  | Plus of lhs:Expression * rhs:Expression
  | Value of value:string
  member self.Val =
    let rec eval expr =
      match expr with 
      | Math m -> eval(m.Head)
      | Plus (lhs, rhs) -> eval lhs + eval rhs
      | Value v -> v |> int
    eval self
  
let text = @"<math>
               <plus>
                 <value>2</value>
                 <value>3</value>
               </plus>
             </math>"

let cases = FSharpType.GetUnionCases (typeof<Expression>)
            |> Array.map(fun f -> 
              (f.Name, FSharpValue.PreComputeUnionConstructor(f)))
            |> Map.ofArray
            
let makeCamelCase (text:string) =
  Char.ToUpper(text.Chars(0)).ToString() + text.Substring(1)
            
let rec recursiveBuild (root:XElement) =
  let name = root.Name.LocalName |> makeCamelCase
  
  let makeCase parameters =
    try
      let caseInfo = cases.Item name
      (caseInfo parameters) :?> Expression
    with 
    | exp -> raise <|
             Exception(String.Format("Failed to create {0} : {1}",
                                     name, exp.Message))
  
  let elems = root.Elements() |> Seq.toArray
  let values = elems |> Array.map(fun f -> recursiveBuild f) 
  if elems.Length = 0 then
    let rootValue = root.Value.Trim()
    makeCase [| box rootValue |]
  else
    try
      values |> Array.map box |> makeCase
    with 
    | _ -> makeCase [| values |> Array.toList |]
    
let rec print expr =
  match expr with 
  | Math m -> print m.Head
  | Plus (lhs, rhs) -> String.Format("({0}+{1})", print lhs, print rhs)
  | Value v -> v
    
let rec eval expr =
  match expr with 
  | Math m -> eval m.Head
  | Plus (lhs, rhs) -> eval lhs + eval rhs
  | Value v -> v |> int

//[<EntryPoint>]
let main argv =
  use stringReader = new StringReader(text)
  use xmlReader = XmlReader.Create(stringReader)
  let doc = XDocument.Load(xmlReader)
  
  let parsed = recursiveBuild doc.Root 
  printf "%s = %d" (print parsed) (eval parsed)

  0 