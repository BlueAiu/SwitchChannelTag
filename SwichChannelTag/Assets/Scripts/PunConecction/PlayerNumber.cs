using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    [SerializeField] PhotonView photonView;

    const int numDigit = 1000;

    public int PlayerNum
    {
        get => photonView.ViewID / numDigit;
        //set { photonView.ViewID = value * numDigit; }
    }
}
