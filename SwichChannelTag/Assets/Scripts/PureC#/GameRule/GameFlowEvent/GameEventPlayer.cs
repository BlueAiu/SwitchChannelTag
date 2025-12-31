using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームイベントの再生処理をする側

public abstract class GameEventPlayer : MonoBehaviour
{
    public abstract bool IsFinished();//イベント処理が終わったか
    public abstract void Play();//イベント処理開始
}
