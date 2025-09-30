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
    CanShift[] _canShifts;


    public void OnExit(MapTransform myMapTrs)//マスから出ていく時(自分のマスを書き換える前に呼ぶ)
    {
        int[] overlapPlayersIndexs= { };//重なっているプレイヤーを取得(☆後に番号を取得するようにする)
        Vector3 massCenterPos = myMapTrs.CurrentWorldPos;//マスの中心点を取得
        int offsetIndex=0;

        //自分以外の同じマスのプレイヤーの位置をずらす
        for (int i=0; i<overlapPlayersIndexs.Length ;i++)
        {
            int overlapIndex=overlapPlayersIndexs[i];

            if (overlapIndex == PlayersManager.MyIndex) continue;//自分だったらずらさない

            else if (_canShifts[i].IsShiftAllowed)//それ以外の人ならずらしてもいいならずらす
            {
                Vector3 pos = massCenterPos + _offsets[offsetIndex];//移動位置
                _setTransforms[i].Position = pos;

                offsetIndex++;
            }
        }
    }

    public void OnEnter(MapTransform myMapTrs)//マスに到着した時(自分のマスを書き換えてから呼ぶ)
    {
        int[] overlapPlayersIndex = { };//重なっているプレイヤーを取得(☆後に番号を取得するようにする)
        Vector3 massCenterPos = myMapTrs.CurrentWorldPos;//マスの中心点を取得
        int offsetIndex = overlapPlayersIndex.Length-1;

        //自分の位置をずらす
        Vector3 pos = massCenterPos + _offsets[offsetIndex];//移動位置
        _setTransforms[PlayersManager.MyIndex].Position = pos;
    }


    //private
    private void Awake()
    {
        _canShifts = PlayersManager.GetComponentsFromPlayers<CanShift>();
        _setTransforms = PlayersManager.GetComponentsFromPlayers<SetTransform>();
    }
}
