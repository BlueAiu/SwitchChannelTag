using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//作成者:杉山
//プレイヤーの番号に合わせて、特徴を反映させる

public class SetPlayersFeature : MonoBehaviour
{
    [SerializeField]
    PlayersFeature _playersFeature;

    //プレイヤーリストに変更があった場合、特徴の変更を行う
    public void SetFeature()
    {
        if (PlayersManager.PlayerInfos.Length > _playersFeature.Features.Length)
        {
            Debug.Log("設定されているプレイヤーの特徴数が足りません。");
            return;
        }

        //自分の名前のみ変更
        PhotonNetwork.NickName = _playersFeature.Features[PlayersManager.MyIndex].name;

        //プレイヤーたちの色を変更
        SetPlayersColor();
    }

    void SetPlayersColor()
    {
        var changeMyColors = PlayersManager.GetComponentsFromPlayers<ChangeMyColor>();

        for(int i=0; i<changeMyColors.Length ;i++)
        {
            changeMyColors[i].SetColor(_playersFeature.Features[i].playerModelColor);
        }
    }
}
