using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//アニメーション関係の汎用処理関係

public static class AnimationUtility
{
    //そのステートが再生し終わるのにかかる時間を取得
    public static float GetClipLength(Animator animator,string stateName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == stateName)
                return clip.length;
        }

        // 見つからなかった場合
        return 0f;
    }
}
