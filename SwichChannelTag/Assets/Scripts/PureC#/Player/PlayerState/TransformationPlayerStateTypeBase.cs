using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの状態(鬼か逃げかの)の変化

public abstract class TransformationPlayerStateTypeBase : MonoBehaviour
{
    public abstract void ChangePlayerState(EPlayerState newState);
}
