using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntryAndExit : MonoBehaviour
{
    [SerializeField] PhotonView p_view;

    void Start()
    {
        ItemWorldManager.AddItem(gameObject);
    }

    private void OnDestroy()
    {
        ItemWorldManager.RemoveItem(gameObject);
    }

    public void RequestDestroy()
    {
        p_view.RPC(nameof(DestroyOwn), RpcTarget.MasterClient);
    }

    [PunRPC]
    void DestroyOwn()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
