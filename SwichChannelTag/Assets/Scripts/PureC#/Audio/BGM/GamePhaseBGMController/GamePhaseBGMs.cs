using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームのフェーズごとのBGM

[CreateAssetMenu(fileName = "GamePhaseBGMs", menuName = "ScriptableObjects/GamePhaseBGMs")]
public class GamePhaseBGMs : ScriptableObject
{
    [Tooltip("keyに逃げの残り人数を入れてください\n配列の順番はバラバラでも大丈夫です。(初期化段階で自動で降順に並び替えます)")] [SerializeField]
    ThresholdTable<AudioClip> _bgmTable;

    public void Init()
    {
        _bgmTable.Init();
    }

    public AudioClip TryGet(int runnersNum)
    {
        return _bgmTable.TryGet(runnersNum);
    }
}
