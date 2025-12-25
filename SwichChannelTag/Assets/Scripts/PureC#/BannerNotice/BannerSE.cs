using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//バナーの効果音処理関係

[System.Serializable]
public class BannerSE
{
    [SerializeField]
    AudioSource _audioSource;

    [Tooltip("バナーの効果音\n設定されていなければ音が鳴らない")] [SerializeField]
    AudioClip _bannerSE;

    public void Play()
    {
        if (_bannerSE == null) return;

        _audioSource.PlayOneShot(_bannerSE);
    }
}
