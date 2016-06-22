namespace Common

module Logging =
    
    open System.Web.Http
    open Serilog

    let configure (config : HttpConfiguration) =
        config.IncludeErrorDetailPolicy <- IncludeErrorDetailPolicy.Always
        Log.Information("Configured HTTP to always include error details")
        config