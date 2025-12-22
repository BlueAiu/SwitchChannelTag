using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーデータの音量部分

public partial class PlayerDataManager
{
    //---音量関係---//
    readonly static Dictionary<ESoundType, string> _soundTypeNameDic
        = new Dictionary<ESoundType, string>(){
            { ESoundType.Master,"MASTER_VOLUME"},//マスター音量のデータ名
            { ESoundType.SE,"SE_VOLUME"},//SE音量のデータ名
            { ESoundType.BGM,"BGM_VOLUME"},//BGM音量のデータ名
    };

    public static SoundVolumeData GetSoundVolume(ESoundType soundType)//音量データの取得(一度も書き換えたことがない場合はnullを返す)
    {
        if (!IsValidSoundType(soundType, out var volumeName)) return null;

        SoundVolumeData ret=null;

        if(PlayerPrefs.HasKey(volumeName))
        {
            float volume = PlayerPrefs.GetFloat(volumeName); ;
            ret = new SoundVolumeData(volume);
        }

        return ret;
    }

    public static void SetSoundVolume(ESoundType soundType,float volume)//音量データの書き換え
    {
        if (!IsValidSoundType(soundType, out var volumeName)) return;   

        PlayerPrefs.SetFloat(volumeName, volume);
    }

    static bool IsValidSoundType(ESoundType soundType, out string volumeName)//存在する音の種類か見る(なかったら警告)
    {
        if (!_soundTypeNameDic.TryGetValue(soundType,out volumeName))
        {
            Debug.Log("処理に失敗");
            return false;
        }

        return true;
    }

    
}
