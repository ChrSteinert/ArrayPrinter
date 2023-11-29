# Array Printer

[![NuGet Status](https://img.shields.io/nuget/v/ArrayPrinter.svg?style=flat)](https://www.nuget.org/packages/ArrayPrinter/)

A F# library to format byte arrays in a human readable fashion.

F# does not provide an easy method of visualizing byte arrays (that I am aware of).
For instance the output via `printfn "%A"` of an array like 

```fs
"eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ" |> System.Convert.FromBase64String
``` 

looks like this:

```fs
[|123uy; 34uy; 115uy; 117uy; 98uy; 34uy; 58uy; 34uy; 49uy; 50uy; 51uy; 52uy;
  53uy; 54uy; 55uy; 56uy; 57uy; 48uy; 34uy; 44uy; 34uy; 110uy; 97uy; 109uy;
  101uy; 34uy; 58uy; 34uy; 74uy; 111uy; 104uy; 110uy; 32uy; 68uy; 111uy; 101uy;
  34uy; 44uy; 34uy; 105uy; 97uy; 116uy; 34uy; 58uy; 49uy; 53uy; 49uy; 54uy; 50uy;
  51uy; 57uy; 48uy; 50uy; 50uy; 125uy|]
```

The Array Printer Library provides a method for pretty printing such arrays.

```fs
ArrayPrinter.printArray PrinterOptions.defaults myArray
```

returns the string

```
0000  7B 22 73 75 62 22 3A 22 31 32 33 34 35 36 37 38  {"sub":"12345678
0010  39 30 22 2C 22 6E 61 6D 65 22 3A 22 4A 6F 68 6E  90","name":"John
0020  20 44 6F 65 22 2C 22 69 61 74 22 3A 31 35 31 36  .Doe","iat":1516
0030  32 33 39 30 32 32 7D                             239022}
```

# Usage

<NuGet>

The output can be controlled via the `ArrayPrinter.PrinterOptions`. 
Defaults for Hex, Decimal, Octal and Binary display are provided via `PrinterOptions.*Defaults`. 
`PrinterOptions.defaults` uses Hex.

Row headers, the array content and a data interpretation of that content are the three parts of the output.
The row headers and the data interpretation can be disabled:

```fs
// Disable the data part.
ArrayPrinter.printArray ({ PrinterOptions.defaults with WithData = false }) myArray

// Disable the header part.
ArrayPrinter.printArray ({ PrinterOptions.defaults with WithHeader = false }) myArray
```

Additionally, the width of a row can be adjusted:

```fs
// Use looong rows
ArrayPrinter.printArray ({ PrinterOptions.defaults with RowWidth = 32 }) myArray
```

The defaults are 16 for hex, 10 for decimal, 8 for ocal and 4 for binary (because it is so large…).
