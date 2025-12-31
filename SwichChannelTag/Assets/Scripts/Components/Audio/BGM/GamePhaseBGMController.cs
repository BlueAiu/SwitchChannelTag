using System.Collections;
using UnityEngine;

//作成者:杉山
//ゲームの状態(残りの逃げの人数)によってBGMを変更する

public class GamePhaseBGMController : MonoBehaviour
{
    [Tooltip("BGMが切り替わる際のフェード時間（秒）")] [SerializeField] 
    float _fadeDuration = 1f;

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
        //逃げの人数を取得
        int runnersCount = GameStatsManager.Instance.PlayersStateStats.GetPlayersCount(EPlayerState.Runner);

        AudioClip nextBgm = _gamePhaseBGMs.TryGet(runnersCount);

        // BGMが存在しない or 既に再生中なら何もしない
        if (nextBgm == null || nextBgm == _audioSource.clip)
            return;

        StartCoroutine(FadeAndChangeBGM(nextBgm));
    }

    // フェードアウト → BGM切り替え → フェードイン
    private IEnumerator FadeAndChangeBGM(AudioClip nextBgm)
    {
        float defaultVolume = _audioSource.volume;
        float halfFadeTime = _fadeDuration * 0.5f;

        // フェードアウト
        yield return FadeVolume(defaultVolume, 0f, halfFadeTime);

        ChangeBGM(nextBgm);

        // フェードイン
        yield return FadeVolume(0f, defaultVolume, halfFadeTime);
    }

    // 指定時間で音量を補間する
    private IEnumerator FadeVolume(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        _audioSource.volume = to;
    }

    private void ChangeBGM(AudioClip bgm)
    {
        _audioSource.Stop();
        _audioSource.clip = bgm;
        _audioSource.Play();
    }
}
