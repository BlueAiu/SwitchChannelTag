using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーのターンフローの状態(Enum型)

public enum EPlayerTurnFlowState
{
    None=-1,//実質nullの扱い

    SelectAction,//行動選択ステート
    Dice,//ダイスステート
    MoveCursor,//カーソル移動ステート
    MovePlayer,//プレイヤー移動ステート
    SelectHierarchy,//階層選択ステート
    ChangeHierarchy,//階層移動ステート
    Finish,//行動終了ステート
    Waiting,//待ち時間ステート
    DiceAnimation,//ダイスアニメーション

    Length
}