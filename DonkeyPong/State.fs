module Game.State

open Elmish.XamarinForms
open Types

let private getPlayers count =
    seq { for i in 1..count do 
            yield { 
                Name = "Player " + i.ToString() 
                Score = None
                IsPlaying = true
                } 
        }

let init () : Model * Cmd<Msg> =
  {
      GameState = New
      Players = getPlayers 6
      Winner = None
  }, []

let incPlayerScore player =
    let curScoreLength, score = 
        match player.Score with
        | None -> 0, ""
        | Some score -> score.Length, score
    if curScoreLength < FinalScoreLength then
        { player with 
                    Score = Some (score + FinalScore.[curScoreLength].ToString()) 
                    IsPlaying = (curScoreLength + 1) < FinalScoreLength
        }
    else
        player
        

let update msg model : Model * Cmd<Msg> =
  match msg with
  | StartNew -> 
    init()
  | AddScore player -> 
    if model.GameState = Over then
        model, []
    else
        let players = 
            model.Players 
            |> Seq.choose (fun x -> if x = player then Some (incPlayerScore x) else Some x)
        let playersPlaying = 
            players 
            |> Seq.filter (fun x -> x.IsPlaying)
            |> Seq.length
        { model with
                    GameState = if playersPlaying = 1 then Over else Active
                    Players = players 
                    Winner = if playersPlaying = 1 then players |> Seq.tryFind(fun x -> x.IsPlaying) else None
        }, []
