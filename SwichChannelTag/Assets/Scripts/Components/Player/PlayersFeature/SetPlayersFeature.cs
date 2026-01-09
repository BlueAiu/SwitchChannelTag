using Photon.Pun;
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
        var changeMyMaterials = PlayersManager.GetComponentsFromPlayers<ChangeMyMaterial>();

        for(int i=0; i< changeMyMaterials.Length ;i++)
        {
            Material[] tagger = _playersFeature.Features[i].taggerModelMaterials;
            Material[] runner = _playersFeature.Features[i].runnerModelMaterials;

            changeMyMaterials[i].SetColorMaterials(tagger,runner);
        }
    }
}
