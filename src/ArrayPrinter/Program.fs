
let query = "MXsBAAABAAAAAAAABWZyaXR6A2JveAAAAQAB" |> System.Convert.FromBase64String

[<EntryPoint>]
let main _ =
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.defaults) query |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.ocalDefaults) query |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.decimalDefaults) query |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.binaryDefaults) query |> printfn "%s"
    0
