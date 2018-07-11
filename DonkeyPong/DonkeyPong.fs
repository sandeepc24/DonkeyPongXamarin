namespace DonkeyPong

open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
open Xamarin.Forms
open Game.State
open Game.Types

module App = 
    let view (model: Model) dispatch =
        Xaml.ContentPage(
          content=Xaml.StackLayout(padding=20.0,
            children=[ 
              yield 
                Xaml.StackLayout(padding=0.0, verticalOptions=LayoutOptions.Center,
                  children=[
                    for player in model.Players do
                        yield Xaml.StackLayout(padding=0.0, orientation=StackOrientation.Horizontal, horizontalOptions=LayoutOptions.Center,
                            children = [ yield Xaml.Label(text = player.Name + " Score: " + (player.Score |> Option.defaultValue ""))
                                         yield Xaml.Button(text = " + ", heightRequest = 40., command = fun () -> dispatch (AddScore player))
                                         ])
                  ])
              if model.GameState = Over then 
                yield Xaml.Label(text = "Winner is: " + (model.Winner.Value.Name))
                yield Xaml.Button(text="Reset", horizontalOptions=LayoutOptions.Center, command=fixf(fun () -> dispatch StartNew))
            ]))

type App () as app = 
    inherit Application ()

    let program = Program.mkProgram init update App.view
    let runner = 
        program
        |> Program.runWithDynamicView app