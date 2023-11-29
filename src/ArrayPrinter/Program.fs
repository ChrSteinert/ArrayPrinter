
open System
open System.IO

open ArrayPrinter

[<EntryPoint>]
let main _ =
    use in' = Console.OpenStandardInput()
    use mem = new MemoryStream()
    in'.CopyTo mem
    mem.ToArray ()
    |> ArrayPrinter.printArray PrinterOptions.defaults
    |> printfn "%s"
    0
