using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public static class PlayersManager
{
    static List<PlayerInfo> players = new();
    static PlayerInfo minePlayer = null;

    static Dictionary<int, PlayerInfo> _actorNumberPlayresDict=new();


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

    public static PlayerInfo MinePlayerInfo
    {
        get => minePlayer;
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

    public static PlayerInfo[] PlayerInfos
    {
        get => players.ToArray();
    }


    // --- actorNumberからプレイヤーの情報を取得 ---//

    public static PlayerInfo ActorNumberPlayerInfo(int actorNumber)//actorNumberに対応したプレイヤーの情報を取得(見つからなかったらnullを返す)
    {
        _actorNumberPlayresDict.TryGetValue(actorNumber, out PlayerInfo ret);
        return ret;
    }


    // --- Add & Remove --- //

    public static void AddPlayer(GameObject player)
    {
        var playerInfo = new PlayerInfo
            (player, player.GetPhotonView().Owner, player.GetComponent<GetPlayerInfo>());

        if (players.Contains(playerInfo)) return;
        players.Add(playerInfo);
        _actorNumberPlayresDict.Add(playerInfo.Player.ActorNumber, playerInfo);
        SortByActorNumber();

        if (playerInfo.Player.IsLocal)
        {
            minePlayer = playerInfo;
        }
    }

    public static void RemovePlayer(GameObject player)
    {
        int actorNumber = player.GetPhotonView().Owner.ActorNumber;

        _actorNumberPlayresDict.TryGetValue(actorNumber,out PlayerInfo playerInfo);

        if (playerInfo == null) return;
        if (!players.Contains(playerInfo)) return;
        players.Remove(playerInfo);
        _actorNumberPlayresDict.Remove(playerInfo.Player.ActorNumber);
        SortByActorNumber();

        if (playerInfo.Player.IsLocal)
        {
            minePlayer = playerInfo;
        }
    }

    static void SortByActorNumber()
    {
        players.Sort((a, b) => 
        a.Player.ActorNumber.CompareTo(
            b.Player.ActorNumber));
    }
}
