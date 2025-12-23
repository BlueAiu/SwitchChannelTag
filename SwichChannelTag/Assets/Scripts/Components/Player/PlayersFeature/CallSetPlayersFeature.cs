using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーオブジェクト側につけて、PlayersManagerに追加・削除されたときにSetPlayersFeatureの処理を呼ぶ

public class CallSetPlayersFeature : MonoBehaviour
{
    [SerializeField]
    PlayerEntryAndExit _playersEntryAndExit;

    SetPlayersFeature _setPlayersFeature;

    private void Awake()
    {
        _setPlayersFeature = GameObject.FindWithTag(TagDictionary.lobbyBehaviour).GetComponent<SetPlayersFeature>();
        _playersEntryAndExit.OnEntry += SetFeature;
        _playersEntryAndExit.OnExit += SetFeature;
    }

    void SetFeature()
    {
        if (_setPlayersFeature == null) return;
        _setPlayersFeature.SetFeature();
    }
}
