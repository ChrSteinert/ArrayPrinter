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

# What can be built with it

This repository contains the ArrayPrinter.Console.
A simple console application that reads from stdin, and feeds that into the `ArrayPrinter`.
Bind the binary to (for instance) `ap` and you could do something like this:

```fish
~/A/s/ArrayPrinter (main)> cat Program.fs | ap
0000  EF BB BF 0D 0A 6F 70 65 6E 20 53 79 73 74 65 6D  ï»¿..open.System
0010  0D 0A 6F 70 65 6E 20 53 79 73 74 65 6D 2E 49 4F  ..open.System.IO
0020  0D 0A 0D 0A 6F 70 65 6E 20 41 72 72 61 79 50 72  ....open.ArrayPr
0030  69 6E 74 65 72 0D 0A 0D 0A 5B 3C 45 6E 74 72 79  inter....[.Entry
0040  50 6F 69 6E 74 3E 5D 0D 0A 6C 65 74 20 6D 61 69  Point.]..let.mai
0050  6E 20 5F 20 3D 0D 0A 20 20 20 20 75 73 65 20 69  n._........use.i
0060  6E 27 20 3D 20 43 6F 6E 73 6F 6C 65 2E 4F 70 65  n'...Console.Ope
0070  6E 53 74 61 6E 64 61 72 64 49 6E 70 75 74 28 29  nStandardInput()
0080  0D 0A 20 20 20 20 75 73 65 20 6D 65 6D 20 3D 20  ......use.mem...
0090  6E 65 77 20 4D 65 6D 6F 72 79 53 74 72 65 61 6D  new.MemoryStream
00A0  28 29 0D 0A 20 20 20 20 69 6E 27 2E 43 6F 70 79  ()......in'.Copy
00B0  54 6F 20 6D 65 6D 0D 0A 20 20 20 20 6D 65 6D 2E  To.mem......mem.
00C0  54 6F 41 72 72 61 79 20 28 29 0D 0A 20 20 20 20  ToArray.()......
00D0  7C 3E 20 41 72 72 61 79 50 72 69 6E 74 65 72 2E  ...ArrayPrinter.
00E0  70 72 69 6E 74 41 72 72 61 79 20 50 72 69 6E 74  printArray.Print
00F0  65 72 4F 70 74 69 6F 6E 73 2E 64 65 66 61 75 6C  erOptions.defaul
0100  74 73 0D 0A 20 20 20 20 7C 3E 20 70 72 69 6E 74  ts.........print
0110  66 6E 20 22 25 73 22 0D 0A 20 20 20 20 30 0D 0A  fn."%s"......0..
```