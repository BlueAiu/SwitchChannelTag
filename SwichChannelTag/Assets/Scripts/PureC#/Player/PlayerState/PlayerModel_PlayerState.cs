using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの状態に応じてモデルを切り替える

public partial class PlayerState
{
    [System.Serializable]
    class PlayerModel_PlayerState
    {
        [Tooltip("鬼のモデル")] [SerializeField]
        GameObject _taggerModel;

        [Tooltip("逃げのモデル")] [SerializeField]
        GameObject _runnerModel;

        public void ChangeMaterial(EPlayerState newState)
        {
            if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
            {
                Debug.Log("存在しない状態です");
                return;
            }

            //モデルの変更
            _taggerModel.SetActive(newState== EPlayerState.Tagger);
            _runnerModel.SetActive(newState== EPlayerState.Runner);
        }
    }
}
