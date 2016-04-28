namespace Bristech.Srm.HttpConfig

open Owin
open System.Web.Http

module Default = 
    let config =
        new HttpConfiguration()
        |> Logging.configure
        |> Cors.configure
        |> Routes.configure
        |> Serialization.configure

type Startup() =
    member __.Configuration (appBuilder: IAppBuilder) = appBuilder.UseWebApi(Default.config) |> ignore