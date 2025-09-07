using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public static class PlayersManager
{
    static List<GetPlayerInfo> players = new();


    // --- Getter --- //

    public static GameObject[] PlayersGameObject
    { 
        get 
        {
            List<GameObject> ret = new();

            foreach (var p in players) 
            {
                if(p == null) continue;

                ret.Add(p.gameObject); 
            }

            return ret.ToArray();
        }
    }

    public static Photon.Realtime.Player[] PlayersPhotonPlayer
    {
        get
        {
            List<Photon.Realtime.Player> ret = new();

            foreach (var p in players)
            {
                if (p == null) continue;

                ret.Add(p.gameObject.GetPhotonView().Owner);
            }

            return ret.ToArray();
        }
    }

    public static T[] GetComponentsFromPlayers<T>() where T : Component
    {
        
        List<T> ret = new();

        foreach (var i in players)
        {
            if (i == null) continue;

            T comp = i.GetComp<T>();

            if (comp != null) { ret.Add(comp); }
        }

        return ret.ToArray();
    }


    // --- Add & Remove --- //

    public static void AddPlayer(GameObject player)
    {
        var playerInfo = player.GetComponent<GetPlayerInfo>();

        if (players.Contains(playerInfo)) return;
        players.Add(playerInfo);
        SortByActorNumber();
    }

    public static void RemovePlayer(GameObject player)
    {
        var playerInfo = player.GetComponent<GetPlayerInfo>();

        if (!players.Contains(playerInfo)) return;
        players.Remove(playerInfo);
        SortByActorNumber();
    }

    static void SortByActorNumber()
    {
        players.Sort((a, b) => 
        a.gameObject.GetPhotonView().Owner.ActorNumber.CompareTo(
            b.gameObject.GetPhotonView().Owner.ActorNumber));
    }
}
