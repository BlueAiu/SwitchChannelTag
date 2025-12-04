using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShowPlayerReady : MonoBehaviourPunCallbacks
{
    /*[SerializeField] GameObject[] Ready_Massage;

    private int Player_Number;
    private int index;
    private int Left_count = 0;

    private void Start()
    {
        if(Ready_Massage == null)
        {
            Debug.LogError("The text is not attached.");
            return;
        }
        for(int i = 0; i < Ready_Massage.Length; i++)
        {
            Ready_Massage[i].SetActive(false);
        }

        UpdatePlayerNum();
    }

    public void ShowReady()
    {
        if(Player_Number < 0)
        {
            Debug.LogError("the value is null");
            return;
        }

        //index = PlayersManager.MyIndex;
        index = Left_count;

        Debug.Log("Player Index: " + index);

        var readyText = Ready_Massage[index];

        bool isActive = !readyText.activeSelf;
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, index, isActive);
        Debug.Log($"ShowReady: player {index} -> {(isActive ? "shown" : "hidden")}");
    }

    [PunRPC]
    private void RPC_ShowReady(int index, bool active)
    {
       var readyText = Ready_Massage[index];

        readyText.SetActive(active);
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerNum();
    }

    public override void OnLeftRoom()
    {
        if (Ready_Massage[index].activeSelf)
        {
            photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, index, false);
            Left_count = 0;
        }
        else
        {
            Left_count = 0;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("index: " + index);
        if (Ready_Massage[index].activeSelf)
        {
            photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, index, false);
            for (int i = 0; i < Ready_Massage.Length; i++)
            {
                Ready_Massage[i].SetActive(false);
            }
            UpdatePlayerNum();
            //Ready_Massage[Left_count].SetActive(true);
            Debug.Log("Left_count: " + Left_count);
            photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, Left_count, true);
        }
        else
        {
            UpdatePlayerNum();
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerNum();
    }

    //自分より先にルーム内にいるプレイヤーをカウントする関数
    private void UpdatePlayerNum()
    {
        if(!PhotonNetwork.InRoom || PhotonNetwork.LocalPlayer == null)
        {
            Left_count = -1;
            Debug.Log("UpdatePlayerNum: Not in room or local player is null.");
            return;
        }

        int myActor = PhotonNetwork.LocalPlayer.ActorNumber;

        Left_count = PhotonNetwork.PlayerList.Count(p => p.ActorNumber < myActor);

        index = Left_count;
    }*/

    [SerializeField] GameObject Ready_Massage;

    private GettingReady mygettingReady;
    private bool Active_State;

    private void Start()
    {
        if(Ready_Massage == null)
        {
            Debug.LogError("The text is not attached.");
            return;
        }

        Ready_Massage.SetActive(false);

        if(!photonView.IsMine)
        {
            Ready_Massage.SetActive(false);
            enabled = false;
            return;
        }

        mygettingReady = null;
        try
        {
            mygettingReady = PlayersManager.GetComponentFromMinePlayer<GettingReady>();
        }
        catch
        {
            //Debug.LogError("Null");
            return;
        }

        Active_State = mygettingReady != null && mygettingReady.IsReady;
        Ready_Massage.SetActive(Active_State);
    }

    private void Update()
    {
        if (Active_State != mygettingReady.IsReady)
        {
            ShowReady();
            Active_State = mygettingReady.IsReady;
        }
    }

    private void ShowReady()
    {
        bool Is_Ready = mygettingReady.IsReady;

        /*if(Is_Ready)
        {
            Debug.Log("Ready!");
        }*/
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, Is_Ready);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, Active_State);
    }

    /*[PunRPC]
    private void RPC_ShowReady(bool active)
    {
       Ready_Massage.SetActive(active);
    }*/
}
