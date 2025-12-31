using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLocation : MonoBehaviour
{
    [SerializeField] Transform WaitingPoint;
    [SerializeField] Vector3 shift;


    void Start()
    {
        var players = PlayersManager.PlayersGameObject;

        for (int i = 0; i < players.Length; i++)
        {
            var player_trs = players[i].transform;
            player_trs.position = WaitingPoint.position + shift * i;
        }
    }
}
