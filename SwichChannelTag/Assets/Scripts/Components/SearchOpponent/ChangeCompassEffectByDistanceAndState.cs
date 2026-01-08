using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//コンパスのエフェクトを距離とプレイヤーの状態(鬼か逃げか)によって変える機能

public class ChangeCompassEffectByDistanceAndState : MonoBehaviour
{
    [Tooltip("遠い場合のエフェクトを0に置くようにしてください")] [SerializeField]
    SerializableDictionary<EPlayerState, GameObject[]> _effects;

    [SerializeField] [Tooltip("距離が小さい時の閾値")]
    public float _nearDistance = 2f;

    [SerializeField] [Tooltip("距離が大きい時の閾値")]
    public float _farDistance = 4f;

    const int farIndex = 0;
    const int mediumIndex = 1;
    const int nearIndex = 2;
    const int overLapIndex = 3;

    private void Start()
    {
        HideAllEffect();
    }

    public void RefleshEffect(EPlayerState state,float distance)
    {
        int index;

        if (distance > _farDistance) { index = farIndex; }
        else if (distance > _nearDistance) { index = mediumIndex; }
        else if (distance > float.Epsilon) { index = nearIndex; }
        else { index = overLapIndex; }

        HideAllEffect();

        var effect = TryGetEffect(state, index);

        if (effect == null) return;

        effect.SetActive(true);
    }

    public void HideAllEffect()
    {
        HideStateEffect(EPlayerState.Tagger);//鬼のコンパスのエフェクトを非表示に

        HideStateEffect(EPlayerState.Runner);//逃げのコンパスのエフェクトを非表示に
    }

    GameObject TryGetEffect(EPlayerState state,int index)
    {
        if (!_effects.TryGetValue(state, out var effects)) return null;

        if (index < 0 || index >= effects.Length) return null;

        return effects[index];
    }

    void HideStateEffect(EPlayerState state)
    {
        if (!_effects.TryGetValue(state, out var stateEffects)) return;

        if (stateEffects == null) return;

        foreach (var stateEffect in stateEffects)
        {
            if (stateEffect == null) continue;

            stateEffect.SetActive(false);
        }
    }
}
