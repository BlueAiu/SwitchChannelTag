using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//バナー通知のマネージャー

public class BannerNoticeManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _bannerText;

    [Tooltip("バナーのアニメーション関係")] [SerializeField]
    BannerAnimation _bannerAnimation;

    [Tooltip("バナーの効果音関係")] [SerializeField]
    BannerSE _bannerSE;

    [Tooltip("表示のために待つ時間")] [SerializeField]
    float _waitToShowDuration=2f;

    Queue<string> _noticeQueue = new();
    bool _isFinishedToShow=true;//バナー表示が終わったか

    public void AddNotice(string content)//通知を追加
    {
        _noticeQueue.Enqueue(content);
    }

    private void Update()
    {
        //まだバナー表示が終わっていない or キューに通知がたまっていない場合はバナー通知を表示しない
        if (!_isFinishedToShow || _noticeQueue.Count == 0) return;
        
        _isFinishedToShow = false;
        StartCoroutine(BannerEnterExitCoroutine(_noticeQueue.Dequeue()));
    }

    IEnumerator BannerEnterExitCoroutine(string content)
    {
        _bannerSE.Play();
        _bannerText.text = content;

        yield return _bannerAnimation.SlideIn();//スライドイン

        yield return new WaitForSeconds(_waitToShowDuration);//表示し続ける

        yield return _bannerAnimation.SlideOut();//スライドアウト

        _isFinishedToShow = true;
    }
}
