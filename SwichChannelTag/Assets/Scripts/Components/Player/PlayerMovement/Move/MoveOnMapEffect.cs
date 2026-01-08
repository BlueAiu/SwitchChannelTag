using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ移動時のエフェクト

public class MoveOnMapEffect : MonoBehaviour
{
    [SerializeField]
    IsMovingState _isMovingState;

    [Tooltip("移動効果音用のAudioSource")] [SerializeField]
    AudioSource _moveAudioSource;

    [Tooltip("移動時のエフェクト(パーティクル)")] [SerializeField]
    ParticleSystem _moveParticle;

    private void OnEnable()
    {
        _isMovingState.OnSwitchIsMoving += OnSwitchValue_IsMoving;
    }

    private void OnDisable()
    {
        _isMovingState.OnSwitchIsMoving -= OnSwitchValue_IsMoving;
    }

    void OnSwitchValue_IsMoving(bool isMoving)
    {
        if(isMoving)//歩行開始した時
        {
            _moveParticle.Play();
            _moveAudioSource.Play();
        }
        else//歩行をやめた時
        {
            _moveParticle.Stop();
            _moveAudioSource.Stop();
        }
    }
}
