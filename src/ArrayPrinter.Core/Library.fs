module ArrayPrinter

open System
open System.Text

[<AutoOpen>]
module Core =
  type NumberType =
    | Hex
    | HexLower
    | Octal
    | Decimal
    | Binary

type PrinterOptions =
  {
    WithHeader : bool
    WithData : bool
    NumberFormat : NumberType
    ColumnWidth : byte
    DataPlaceholderChar : char
  }
  
  static member defaults = 
    {
      WithHeader = true
      WithData = true
      NumberFormat = Hex
      ColumnWidth = 16uy
      DataPlaceholderChar = '.'
    }
    
  static member binaryDefaults =
    {
      WithHeader = true
      WithData = true
      NumberFormat = Binary
      ColumnWidth = 4uy
      DataPlaceholderChar = '.'
    }

  static member decimalDefaults =
    {
      WithHeader = true
      WithData = true
      NumberFormat = Decimal
      ColumnWidth = 10uy
      DataPlaceholderChar = '.'
    }

  static member ocalDefaults =
    {
      WithHeader = true
      WithData = true
      NumberFormat = Octal
      ColumnWidth = 8uy
      DataPlaceholderChar = '.'
    }

let private formatHeaderNumber format (value : int) =
  match format with
  | Hex -> value.ToString("X4")
  | HexLower -> value.ToString("x4")
  | Octal -> 
    let c = Convert.ToString(value, 8)
    '0' |> List.replicate (4 - c.Length)
    |> Seq.toArray
    |> String |> (fun d -> d + c)
  | Decimal -> value.ToString("0000")
  | Binary -> 
    let c = Convert.ToString(value, 2)
    '0' |> List.replicate (8 - c.Length)
    |> Seq.toArray
    |> String |> (fun d -> d + c)

let private formatNumber format (value : byte) =
  match format with
  | Hex -> value.ToString("X2")
  | HexLower -> value.ToString("x2")
  | Octal -> 
    let c = Convert.ToString(value, 8)
    if c.Length = 1 then "00" + c
    elif c.Length = 2 then "0" + c
    else c
  | Decimal -> value.ToString("000")
  | Binary -> 
    let c = Convert.ToString(value, 2)
    '0' |> List.replicate (8 - c.Length)
    |> Seq.toArray
    |> String |> (fun d -> d + c)
    
let private emptyCell = 
  function 
  | Hex 
  | HexLower -> "  "
  | Octal
  | Decimal -> "   "
  | Binary -> "        "

let printArray (options : PrinterOptions) (bytes : byte array) =
  let builder = new StringBuilder ()

  for row in [0..bytes.Length / (int options.ColumnWidth)] do
    if options.WithHeader 
    then 
      (row * int options.ColumnWidth) |> formatHeaderNumber options.NumberFormat
      |> builder.Append |> ignore
      // After Row Header Padding
      builder.Append " " |> ignore
      
    for col in [0..(int options.ColumnWidth - 1)] do
      // Before Cell Padding
      builder.Append " " |> ignore
      let i = row * (int options.ColumnWidth) + col
      if i >= bytes.Length then emptyCell options.NumberFormat
      else bytes.[i] |> formatNumber options.NumberFormat
      |> builder.Append
      |> ignore


    if options.WithData then
      // Before Data Padding
      builder.Append "  " |> ignore
      for col in [0..(int options.ColumnWidth - 1)] do
        let i = row * (int options.ColumnWidth) + col
        (
          if i >= bytes.Length then ' '
          else 
            let c = bytes.[i] |> int |> char
            if Char.IsAsciiLetterOrDigit c || Char.IsPunctuation c then c else options.DataPlaceholderChar
        )
        |> builder.Append
        |> ignore

    builder.AppendLine () |> ignore

  builder.ToString ()
