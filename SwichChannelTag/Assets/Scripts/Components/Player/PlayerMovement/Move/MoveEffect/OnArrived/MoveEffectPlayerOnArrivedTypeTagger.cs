using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//鬼のマスに到着した時に行う移動エフェクト関係の処理

public class MoveEffectPlayerOnArrivedTypeTagger : MoveEffectPlayerOnArrivedTypeBase
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    AudioClip _taggerMoveSE;

    public override void Play(MapPos pos)
    {
        _audioSource.PlayOneShot(_taggerMoveSE);
    }
}
