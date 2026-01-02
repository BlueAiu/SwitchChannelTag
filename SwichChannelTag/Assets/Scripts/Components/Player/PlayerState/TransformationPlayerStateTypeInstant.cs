using System;
using UnityEngine;

//作成者:杉山
//即座にプレイヤーの見た目を変化させる

public class TransformationPlayerStateTypeInstant : TransformationPlayerStateTypeBase
{
    [Tooltip("鬼のモデル")] [SerializeField]
    GameObject _taggerModel;

    [Tooltip("逃げのモデル")] [SerializeField]
    GameObject _runnerModel;

    public override void ChangePlayerState(EPlayerState newState)
    {
        if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない状態です");
            return;
        }

        //モデルの変更
        _taggerModel.SetActive(newState == EPlayerState.Tagger);
        _runnerModel.SetActive(newState == EPlayerState.Runner);
    }
}
