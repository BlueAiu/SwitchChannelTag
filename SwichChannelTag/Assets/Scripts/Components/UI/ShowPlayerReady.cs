using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerReady : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject[] Ready_Massage;

    private int PlayerNum;
    // Start is called before the first frame update

    private void Start()
    {
        for(int i = 0; i < Ready_Massage.Length; i++)
        {
            Ready_Massage[i].SetActive(false);
        }
    }

    public void ShowReady()
    {
        PlayerNum = photonView.OwnerActorNr - 1;
        switch(PlayerNum)
        {
            case 0:
                Ready_Massage[0].SetActive(true); break;
            case 1:
                Ready_Massage[1].SetActive(true); break;
            case 2:
                Ready_Massage[2].SetActive(true); break;
            case 3:
                Ready_Massage[3].SetActive(true); break;
        }
    }
}
