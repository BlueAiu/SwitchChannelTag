using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;
using static UnityEngine.CullingGroup;


//作成者:杉山
//ゲームの状態を他プレイヤーにも通知(共有)する機能

public class ObserveGameStateManager : MonoBehaviourPunCallbacks
{
    public static ObserveGameStateManager Instance { get; private set; }

    private const string GAMESTATE_KEY = "GameState";

    public event Action<EGameFlowState> OnStateChanged;

    public EGameFlowState State
    {
        get
        {
            if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GAMESTATE_KEY))
            {
                return (EGameFlowState)PhotonNetwork.CurrentRoom.CustomProperties[GAMESTATE_KEY];
            }

            Debug.Log("ゲームステートの取得に失敗");
            return EGameFlowState.None;
        }

        set
        {
            Hashtable props = new Hashtable();
            props[GAMESTATE_KEY] = value;
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        State = EGameFlowState.None;
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.TryGetValue(GAMESTATE_KEY, out object value))
        {
            OnStateChanged?.Invoke((EGameFlowState)value);
        }
    }
}
