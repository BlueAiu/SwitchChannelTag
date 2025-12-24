using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//バナーのアニメーション

[System.Serializable]
public class BannerAnimation
{
    [SerializeField]
    Animator _bannerAnimator;

    [Tooltip("スライドインのトリガー名")] [SerializeField]
    string _slideInTriggerName;

    [Tooltip("スライドインのアニメーション")] [SerializeField]
    AnimationClip _slideInClip;

    [Tooltip("スライドアウトのトリガー名")] [SerializeField]
    string _slideOutTriggerName;

    [Tooltip("スライドアウトのアニメーション")] [SerializeField]
    AnimationClip _slideOutClip;

    public IEnumerator SlideIn()//スライドイン
    {
        _bannerAnimator.SetTrigger(_slideInTriggerName);
        yield return new WaitForSeconds(AnimationUtility.GetClipLength(_bannerAnimator, _slideInClip.name));
    }

    public IEnumerator SlideOut()//スライドアウト
    {
        _bannerAnimator.SetTrigger(_slideOutTriggerName);
        yield return new WaitForSeconds(AnimationUtility.GetClipLength(_bannerAnimator, _slideOutClip.name));
    }
}
