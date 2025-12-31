//作成者:杉山
//ゲームイベント

public enum EGameEvent
{
    GameStart,//スタート時のイベント
    GameFinish,//ゲーム終了時のイベント
    TaggerTurn,//鬼のターン(終了時)
    RunnerTurn,//逃げのターン(終了時)
    FromLobbyToGame,//ロビーからメインシーンに移る時

    Length//長さ(これ以降に値を入れないでください)
}
