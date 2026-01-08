using UnityEngine;

//作成者:杉山
//移動中であるかの値が切り替わった時に行う移動エフェクト関係の処理

public abstract class MoveEffectPlayerOnSwitchIsMovingTypeBase : MonoBehaviour
{
    public abstract void Play(bool isMoving);
}
