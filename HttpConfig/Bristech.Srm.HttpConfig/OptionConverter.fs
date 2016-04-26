namespace Bristech.Srm.HttpConfig

open System
open Microsoft.FSharp.Reflection
open Newtonsoft.Json

type OptionConverter() =
    inherit JsonConverter()
    
    override __.CanConvert(``type``) = 
        ``type``.IsGenericType && ``type``.GetGenericTypeDefinition() = typedefof<option<_>>

    override __.WriteJson(writer, value, serializer) =
        let newValue = 
            if value |> isNull then null
            else 
                FSharpValue.GetUnionFields(value, value.GetType()) |> snd |> Array.head
        serializer.Serialize(writer, newValue)

    override __.ReadJson(reader, ``type``, _, serializer) =        
        let innerType = ``type``.GetGenericArguments().[0]
        let innerType = 
            if innerType.IsValueType then (typedefof<Nullable<_>>).MakeGenericType([|innerType|])
            else innerType        
        let value = serializer.Deserialize(reader, innerType)
        let cases = FSharpType.GetUnionCases(``type``)
        if value |> isNull then FSharpValue.MakeUnion(cases.[0], [||])
        else FSharpValue.MakeUnion(cases.[1], [|value|])