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


    public void OnExit(MapTransform myMapTrs)//マスから出ていく時(自分のマスを書き換える前に呼ぶ)
    {
        PlayerInfo[] overlapPlayersInfos= _getOverlapPlayer.GetOverlapPlayers();//重なっているプレイヤーを取得(☆後に番号を取得するようにする)
        Vector3 massCenterPos = myMapTrs.CurrentWorldPos;//マスの中心点を取得
        int offsetIndex=0;

        //自分以外の同じマスのプレイヤーの位置をずらす
        for (int i=0; i<overlapPlayersInfos.Length ;i++)
        {
            SetTransform setTransform=overlapPlayersInfos[i].GetComponent<SetTransform>();
            CanShift canShift = overlapPlayersInfos[i].GetComponent<CanShift>();

            if (overlapPlayersInfos[i].Player.IsLocal) continue;//自分だったらずらさない

            else if (canShift.IsShiftAllowed)//それ以外の人ならずらしてもいいならずらす
            {
                Vector3 pos = massCenterPos + _offsets[offsetIndex];//移動位置
                setTransform.Position = pos;

                offsetIndex++;
            }
        }
    }

    public void OnEnter(MapTransform myMapTrs)//マスに到着した時(自分のマスを書き換えてから呼ぶ)
    {
        PlayerInfo[] overlapPlayersInfos = _getOverlapPlayer.GetOverlapPlayers();//重なっているプレイヤーを取得(☆後に番号を取得するようにする)
        Vector3 massCenterPos = myMapTrs.CurrentWorldPos;//マスの中心点を取得
        int offsetIndex = overlapPlayersInfos.Length;
        Debug.Log(offsetIndex);
        SetTransform mySetTrs = PlayersManager.GetComponentFromMinePlayer<SetTransform>();

        //自分の位置をずらす
        Vector3 pos = massCenterPos + _offsets[offsetIndex];//移動位置
        mySetTrs.Position = pos;
    }
}
