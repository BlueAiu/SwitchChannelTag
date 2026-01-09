using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//捕まった時（鬼が逃げに触れた時）のエフェクト
//プレイヤーごとに付ける

public class CaughtEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _caughtParticle;

    [SerializeField]
    AudioSource _caughtAudioSource;

    [SerializeField]
    AnimatorManager _myAnimatorManager;

    bool _hasPlayedEffect = false;

    public bool HasPlayedEffect { get { return _hasPlayedEffect; } }

    public void Clear()
    {
        if(_caughtParticle.isPlaying) _caughtParticle.Stop();
        if(_caughtAudioSource.isPlaying) _caughtAudioSource.Stop();
        _hasPlayedEffect=false;
    }

    public void Play()
    {
        if(_hasPlayedEffect) return;

        _caughtParticle.Play();
        _caughtAudioSource.Play();
        _myAnimatorManager.SetTrigger(PlayerAnimatorParameterNameDictionary.caught);
        _hasPlayedEffect=true;
    }
}
