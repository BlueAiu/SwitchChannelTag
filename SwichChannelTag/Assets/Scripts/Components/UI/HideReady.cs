using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideReady : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Ready_button;
    [SerializeField] GameObject JoiningImage;


    public override void OnJoinedRoom()
    {
        Ready_button.SetActive(true);
        JoiningImage.SetActive(false);
    }
}
