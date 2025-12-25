using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//作成者:杉山
//プレイヤーオブジェクト側につけて、PlayersManagerに追加・削除されたときにSetPlayersFeatureの処理を呼ぶ

public class CallSetPlayersFeature : MonoBehaviour
{
    [SerializeField]
    PlayerEntryAndExit _playersEntryAndExit;

    [SerializeField]
    PhotonView _myPhotonView;

    SetPlayersFeature _setPlayersFeature;

    private void Awake()
    {
        _setPlayersFeature = GameObject.FindWithTag(TagDictionary.lobbyBehaviour).GetComponent<SetPlayersFeature>();
        _playersEntryAndExit.OnEntry += OnEntry;
        _playersEntryAndExit.OnExit += OnExit;
    }

    void OnEntry()
    {
        SetFeature();
    }

    void OnExit()
    {
        //自分が退出したのであれば処理を行わない
        if (_myPhotonView.IsMine) return;

        SetFeature();
    }

    void SetFeature()
    {
        if (_setPlayersFeature == null) return;
        _setPlayersFeature.SetFeature();
    }
}
