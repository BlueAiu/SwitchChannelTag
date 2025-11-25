using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerReady : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject[] Ready_Massage;
    // Start is called before the first frame update

    public override void OnJoinedRoom()
    {
        for(int i = 0; i < Ready_Massage.Length; i++)
        {
            Ready_Massage[i].SetActive(false);
        }
    }


}
