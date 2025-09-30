using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOverlapPlayer : MonoBehaviour
{
    struct PlayerInfoAndPos
    {
        public MapTransform trs;
        public PlayerInfo playerInfo;

        public PlayerInfoAndPos(MapTransform trs, PlayerInfo playerInfo)
        {
            this.trs = trs;
            this.playerInfo = playerInfo;
        }
    }





    [SerializeField] bool writeLog = false;

    PlayerInfoAndPos minePlayer;
    PlayerInfoAndPos[] otherPlayers;

    // Start is called before the first frame update
    void Start()
    {
        var minePlayerInfo = PlayersManager.MinePlayerInfo;
        minePlayer = new PlayerInfoAndPos(minePlayerInfo.GetComponent<MapTransform>() , minePlayerInfo);

        var players = PlayersManager.PlayerInfos;

        List<PlayerInfoAndPos> othersList = new();

        foreach (var player in players)
        {
            var trs = player.GetComponent<MapTransform>();
            var infoAndPos = new PlayerInfoAndPos(trs, player);

            if (player != minePlayerInfo)
            {
                othersList.Add(infoAndPos);
            }
        }

        otherPlayers = othersList.ToArray();
    }

    public PlayerInfo[] GetOverlapPlayers()
    {
        List<PlayerInfo> overlapPlayers = new();
        foreach (var other in otherPlayers)
        {
            if (minePlayer.trs.Pos==other.trs.Pos)
            {
                overlapPlayers.Add(other.playerInfo);
            }
        }

        if (writeLog) Log(overlapPlayers);
        return overlapPlayers.ToArray();
    }
    
    void Log(List<PlayerInfo> objects)
    {
        string log = "GetOverlapPlayers: ";
        foreach (var obj in objects)
        {
            log += obj.GetComponent<PlayerNumber>().PlayerNum + ", ";
        }
        Debug.Log(log);
    }
}
