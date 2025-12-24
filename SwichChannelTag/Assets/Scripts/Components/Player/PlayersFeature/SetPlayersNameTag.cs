using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーのネームタグを変更する

public class SetPlayersNameTag : MonoBehaviour
{
    void Update()
    {
        var showPlayerNameTags = PlayersManager.GetComponentsFromPlayers<ShowPlayerNameTag>();
        var players = PlayersManager.PlayersPhotonPlayer;

        for(int i=0; i<showPlayerNameTags.Length ;i++)
        {
            string nameText = players[i].NickName;

            if (players[i].IsLocal)//自分のであるかを分かるようにする
            {
                nameText += "(You)";
            }

            showPlayerNameTags[i].SetNameText(nameText);
        }
    }
}
