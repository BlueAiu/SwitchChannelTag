using UnityEngine;

//作成者:杉山
//バフエフェクトを表示させる機能(同期機能は無し、プレイヤーに直接付ける)

public class BuffEffectActivator : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _buffParticle;

    bool _isActive = false;

    public void SwitchActivate(bool isActive)//バフエフェクトを表示させる
    {
        if (isActive == _isActive) return;

        _isActive = isActive;

        if (_isActive) _buffParticle.Play();
        else _buffParticle.Stop();
    }
}
