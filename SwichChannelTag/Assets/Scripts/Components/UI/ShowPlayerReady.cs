using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowPlayerReady : MonoBehaviourPunCallbacks
{
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

        if(!photonView.IsMine)
        {
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
            return;
        }

        Active_State = mygettingReady != null && mygettingReady.IsReady;

        SceneManager.sceneLoaded += OnSceneLoaded;
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

        
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, Is_Ready);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, Active_State);
    }

    private void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        photonView.RPC(nameof(RPC_ShowReady), RpcTarget.AllBuffered, false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    [PunRPC]
    private void RPC_ShowReady(bool active)
    {
       Ready_Massage.SetActive(active);
    }
}
