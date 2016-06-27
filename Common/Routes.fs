namespace Common

module Routes =
    
    open System.Web.Http
    open Serilog

    let configure (config : HttpConfiguration) =
        config.MapHttpAttributeRoutes()
        let route = config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}")
        route.Defaults.Add("id", RouteParameter.Optional)
        Log.Information("Configured API routing")
        config
