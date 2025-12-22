using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//作成者:杉山
//音量を初期化する(最初のシーンに置けばゲームを始めた瞬間に音量が設定される)

public class SetSoundVolume : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;

    [SerializeField] SoundConfigs _soundConfigs;

    private void Start()
    {
        //AudioMixerの音量の初期化
        for(int i=0; i<(int)ESoundType.Length ;i++)
        {
            ESoundType type = (ESoundType)i;

            string soundName=AudioMixerName.SoundName(type);

            SoundVolumeData volumeData = PlayerDataManager.GetSoundVolume(type);//音量データの取得

            float volume = (volumeData != null) ? volumeData.Value : _soundConfigs.DefaultVolume(type);

            _audioMixer.SetFloat(soundName,volume);
        }
    }
}
