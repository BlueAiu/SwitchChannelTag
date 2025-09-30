using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤー達の位置をずらす

public class ShiftPlayersPosition : MonoBehaviour
{
    [Tooltip("マスの中心点からどの位置までずらすか")] [SerializeField]
    Vector3[] _offsets;

    [Tooltip("重なっているプレイヤーを取得する機能")] [SerializeField]
    GetOverlapPlayer _getOverlapPlayer;

    SetTransform[] _setTransforms;


    public void OnExit(MapTransform myMapTrs)//マスから出ていく時(自分のマスを書き換える前に呼ぶ)
    {
        int[] overlapPlayersIndexs= { };//重なっているプレイヤーを取得
        Vector3 massCenterPos = myMapTrs.CurrentWorldPos;//マスの中心点を取得

        int offsetIndex=0;

        for(int i=0; i<overlapPlayersIndexs.Length ;i++)//自分以外の同じマスのプレイヤーの位置をずらす
        {
            int overlapIndex=overlapPlayersIndexs[i];

            if (overlapIndex == PlayersManager.MyIndex) continue;//自分だったらずらさない

            if(true)//それ以外の人ならずらしてもいいならずらす
            {
                //マスの中心点にoffsetを足す

                //プレイヤーの位置を動かす

                offsetIndex++;
            }
        }
    }

    public void OnEnter()//マスに到着した時(自分のマスを書き換えてから呼ぶ)
    {
        int[] overlapPlayersIndex = { };//重なっているプレイヤーを取得

        //自分の位置をずらす
    }


    //private
    private void Awake()
    {
        _setTransforms = PlayersManager.GetComponentsFromPlayers<SetTransform>();
    }
}
