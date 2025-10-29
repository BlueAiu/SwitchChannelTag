using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの状態(Enum)

public enum EGameState
{
    None = -1,//実質nullの扱い

    Start,//開始演出ステート
    TaggerTurn,//鬼ターンステート
    RunnerTurn,//逃げターンステート
    TurnFinish,//(お互いの)ターン終了ステート
    Finish,//終了演出ステート

    Length
}