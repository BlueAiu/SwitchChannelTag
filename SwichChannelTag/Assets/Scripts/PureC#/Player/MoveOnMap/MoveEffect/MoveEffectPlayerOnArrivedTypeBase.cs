//作成者:杉山
//マスに着いた時に行う移動エフェクト関係の処理

using UnityEngine;

public abstract class MoveEffectPlayerOnArrivedTypeBase : MonoBehaviour
{
    public abstract void Play(MapPos pos);
}
