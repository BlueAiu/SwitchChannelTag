using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//逃げの移動中のエフェクト

public class MoveEffectPlayerOnSwitchIsMovingTypeRunner : MoveEffectPlayerOnSwitchIsMovingTypeBase
{
    [Tooltip("移動時のエフェクト(パーティクル)")] [SerializeField]
    ParticleSystem _moveParticle;

    public override void Play(bool isMoving)
    {
        if (isMoving)//歩行開始した時
        {
            _moveParticle.Play();
        }
        else//歩行をやめた時
        {
            _moveParticle.Stop();
        }
    }
}
