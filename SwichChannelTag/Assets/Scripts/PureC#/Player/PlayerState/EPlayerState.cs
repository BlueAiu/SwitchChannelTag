using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの状態(鬼か逃げか)のEnum型

public enum EPlayerState
{
    None=-1,//無し(実質Null的な扱い)
    Runner,//逃げ
    Tagger,//鬼

    Length//長さ(これ以降に要素を追加しないでください)
}