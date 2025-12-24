using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//バナー通知のマネージャー

public class BannerNoticeManager : MonoBehaviour
{
    [SerializeField]
    Animator _bannerAnimator;

    [SerializeField]
    TextMeshProUGUI _bannerText;

    [SerializeField]
    string _slideInTriggerName;

    [SerializeField]
    string _slideOutTriggerName;

    [Tooltip("表示のために待つ時間")] [SerializeField]
    float _waitToShowDuration=3.5f;

    [Tooltip("スライドアウトするのを待つ時間")] [SerializeField]
    float _waitToSlideOutDuration = 1.5f;

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
        _bannerText.text = content;
        _bannerAnimator.SetTrigger(_slideInTriggerName);

        yield return new WaitForSeconds(_waitToShowDuration);//表示し続ける

        _bannerAnimator.SetTrigger(_slideOutTriggerName);

        yield return new WaitForSeconds(_waitToSlideOutDuration);//上がりきるまで待つ

        _isFinishedToShow = true;
    }
}
