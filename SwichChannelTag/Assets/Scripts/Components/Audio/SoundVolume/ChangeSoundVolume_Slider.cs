using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//作成者:杉山
//スライダーで音量を変更する

public class ChangeSoundVolume_Slider : MonoBehaviour
{
    [Tooltip("音の種類")] [SerializeField] 
    ESoundType _soundType;

    [Tooltip("使用するスライダー")] [SerializeField] 
    Slider _slider;

    [SerializeField] AudioMixer _audioMixer;

    [SerializeField] SoundConfigs _soundConfigs;

    public void Change(float value)
    {
        string soundName = AudioMixerName.SoundName(_soundType);

        _audioMixer.SetFloat(soundName, value);

        PlayerDataManager.SetSoundVolume(_soundType, value);//セーブデータの方にも記録
    }

    //private
    private void Awake()
    {
        _slider.onValueChanged.AddListener(Change);
    }

    private void Start()
    {
        //スライダーの最大値と最小値を設定
        _slider.maxValue = _soundConfigs.MaxVolume(_soundType);
        _slider.minValue= _soundConfigs.MinVolume(_soundType);

        //セーブデータから現在の音量を取ってきて、スライダーの現在の値を設定
        SoundVolumeData soundVolumeSaveData = PlayerDataManager.GetSoundVolume(_soundType);
        _slider.value = (soundVolumeSaveData != null) ? soundVolumeSaveData.Value : _soundConfigs.DefaultVolume(_soundType);
    }
}
