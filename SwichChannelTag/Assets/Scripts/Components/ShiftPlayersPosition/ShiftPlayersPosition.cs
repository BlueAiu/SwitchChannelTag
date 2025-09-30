using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤー達の位置をずらす

public class ShiftPlayersPosition : MonoBehaviour
{
    [Tooltip("マスの中心点からどの位置までずらすか")] [SerializeField]
    Vector3[] offsets;

    [Tooltip("重なっているプレイヤーを取得する機能")] [SerializeField]
    GetOverlapPlayer _getOverlapPlayer;

    SetTransform[] _setTransform;


    public void OnExit()//マスから出ていく時(自分のマスを書き換える前に読んでください)
    {
        //重なっているプレイヤーを取得

        //自分以外の同じマスのプレイヤーの位置をずらす
    }

    public void OnEnter()//マスに到着した時(自分のマスを書き換えてから読んでください)
    {
        //重なっているプレイヤーを取得

        //自分の位置をずらす
    }


    //private
    private void Awake()
    {
        _setTransform = PlayersManager.GetComponentsFromPlayers<SetTransform>();
    }
}
