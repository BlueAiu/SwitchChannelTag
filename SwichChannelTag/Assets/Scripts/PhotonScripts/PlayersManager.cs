using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public static class PlayersManager
{
    static List<PlayerInfo> players = new();
    static PlayerInfo minePlayer = null;


    // --- Getter --- //

    // MinePlayer

    public static int MyIndex {  get { return players.IndexOf(minePlayer); } }

    public static GameObject MinePlayerGameObject
    {
        get => minePlayer.PlayerObject;
    }

    public static Photon.Realtime.Player MinePlayerPhotonPlayer
    {
        get => minePlayer.Player;
    }

    public static T GetComponentFromMinePlayer<T>() where T : Component
    {
        return minePlayer.GetComponent<T>();
    }

    public static int MinePlayerNumber
    {
        get => players.IndexOf(minePlayer);
    }

    // EveryPlayers

    public static GameObject[] PlayersGameObject
    { 
        get 
        {
            List<GameObject> ret = new();

            foreach (var p in players) 
            {
                if(p == null) continue;

                ret.Add(p.PlayerObject); 
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

                ret.Add(p.Player);
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

            T comp = i.GetComponent<T>();

            if (comp != null) { ret.Add(comp); }
        }

        return ret.ToArray();
    }


    // --- Add & Remove --- //

    public static void AddPlayer(GameObject player)
    {
        var playerInfo = new PlayerInfo
            (player, player.GetPhotonView().Owner, player.GetComponent<GetPlayerInfo>());

        if (players.Contains(playerInfo)) return;
        players.Add(playerInfo);
        SortByActorNumber();

        FindMine();//自分を見つける
    }

    public static void RemovePlayer(GameObject player)
    {
        var playerInfo = new PlayerInfo
            (player, player.GetPhotonView().Owner, player.GetComponent<GetPlayerInfo>());

        if (!players.Contains(playerInfo)) return;
        players.Remove(playerInfo);
        SortByActorNumber();

        FindMine();//自分を見つける
    }

    static void FindMine()//自分を見つける(アクターナンバー順に整列した後に呼ぶ)
    {
        for(int i=0; i<players.Count ;i++)
        {
            var player = players[i];

            if (player.Player== PhotonNetwork.LocalPlayer)
            {
                minePlayer = player;
                return;
            }
        }
    }

    static void SortByActorNumber()
    {
        players.Sort((a, b) => 
        a.Player.ActorNumber.CompareTo(
            b.Player.ActorNumber));
    }
}
