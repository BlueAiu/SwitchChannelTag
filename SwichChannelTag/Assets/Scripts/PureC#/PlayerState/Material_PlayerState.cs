using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

//作成者:杉山
//プレイヤーの状態に応じてマテリアルを切り替える

[System.Serializable]
public class Material_PlayerState
{
    [Tooltip("鬼のマテリアル")] [SerializeField]
    Material _taggerMaterial;

    [Tooltip("逃げのマテリアル")] [SerializeField]
    Material _runnerMaterial;

    [Tooltip("プレイヤーのメッシュ")] [SerializeField]
    MeshRenderer _mesh;

    public void ChangeMaterial(EPlayerState newState)
    {
        if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない状態です");
            return;
        }

        //マテリアルの変更
        switch (newState)
        {
            case EPlayerState.Runner://逃げ
                _mesh.material = _runnerMaterial;
                break;

            case EPlayerState.Tagger://鬼
                _mesh.material = _taggerMaterial;
                break;
        }
    }
}
