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
        Text_PlayerTag.SetActive(photonView.IsMine);
    }
}
