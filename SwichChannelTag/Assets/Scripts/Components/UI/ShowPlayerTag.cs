using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerTag : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Text_PlayerTag;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            Text_PlayerTag.SetActive(true);
        }
        else
        {
            Text_PlayerTag.SetActive(false);
        }
    }
}
