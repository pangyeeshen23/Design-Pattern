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
open System.Diagnostics

let doWork() =
    printf "Doing some work"

let logger work name =
    let sw = Stopwatch.StartNew()
    printfn "%s %s" "Entering function" name
    work()
    sw.Stop()
    printfn "Exiting method %s: %fs elapsed" name sw.Elapsed.TotalSeconds

[<EntryPoint>]
let main argv = 
    let work() = logger doWork "do_work"
    work()
    0