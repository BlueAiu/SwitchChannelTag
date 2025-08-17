using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviourPunCallbacks
{
    List<GameObject> players = new();

    public GameObject[] Players 
    { 
        get 
        {
            ResetPlayersList();
            return players.ToArray();
        }
    }
   
    void ResetPlayersList()
    {
        players.Clear();

        foreach(var i in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(i);
        }
    }

    //プレイヤー達のComponentを配列で取得
    public T[] GetComponentsFromPlayers<T>() where T : Component
    {
        ResetPlayersList();

        List<T> ret = new();

        foreach (var i in players)
        {
            if(i == null) continue;
            T comp = i.GetComponent<T>();
            if (comp != null)
            {
                ret.Add(comp);
            }
        }

        return ret.ToArray();
    }
}
