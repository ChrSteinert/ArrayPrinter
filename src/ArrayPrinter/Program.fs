
open ArrayPrinter

let myArray = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ==" |> System.Convert.FromBase64String

[<EntryPoint>]
let main _ =
    myArray |> printfn "%A"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.defaults) myArray |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.ocalDefaults) myArray |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.decimalDefaults) myArray |> printfn "%s"
    ArrayPrinter.printArray (ArrayPrinter.PrinterOptions.binaryDefaults) myArray |> printfn "%s"
    ArrayPrinter.printArray ({ PrinterOptions.defaults with WithData = false }) myArray |> printfn "%s"
    0
