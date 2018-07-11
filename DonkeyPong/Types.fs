module Game.Types

type Player = {
    Name : string
    Score : string option
    IsPlaying : bool
}

type GameState =
| New
| Active
| Over

let FinalScore = "DONKEY"
let FinalScoreLength = FinalScore.Length

type Game = {
    GameState : GameState
    Players : Player seq
    Winner : Player option
}

type Model = Game

type Msg =
  | StartNew
  | AddScore of Player

