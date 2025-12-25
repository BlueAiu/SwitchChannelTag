using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの状態(残りの逃げの人数によってBGMを変更する)

public class GamePhaseBGMController : MonoBehaviour
{
    [SerializeField]
    GamePhaseBGMs _gamePhaseBGMs;

    [SerializeField]
    AudioSource _audioSource;

    private void Awake()
    {
        _gamePhaseBGMs.Init();
    }

    //ターンが終わるごとにBGMの更新処理を行う
    public void UpdateBGM()
    {
        //プレイヤーの人数を取得
        GameStatsManager.Instance.PlayersStateStats.GetPlayersCount(out int allPlayersCount, out int runnersCount, out int taggersCount);

        AudioClip bgm = _gamePhaseBGMs.TryGet(runnersCount);

        if (bgm == null) return;
        if (bgm == _audioSource.clip) return;

        //BGMが変わったら処理
        _audioSource.Stop();
        _audioSource.clip = bgm;
        _audioSource.Play();
    }
}
