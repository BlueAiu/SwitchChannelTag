using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchOpponentPlayer : MonoBehaviour
{
    struct PlayerStateAndPos
    {
        public MapTransform trs;
        public PlayerState state;

        public PlayerStateAndPos(MapTransform trs, PlayerState state)
        {
            this.trs = trs;
            this.state = state;
        }
    }


    PlayerStateAndPos minePlayer;
    PlayerStateAndPos[] otherPlayers;

    void Awake()
    {
        var minePlayerInfo = PlayersManager.MinePlayerInfo;
        var mineTrs = minePlayerInfo.GetComponent<MapTransform>();
        var mineState = minePlayerInfo.GetComponent<PlayerState>();
        minePlayer = new PlayerStateAndPos(mineTrs, mineState);

        var players = PlayersManager.PlayerInfos;

        List<PlayerStateAndPos> othersList = new();

        foreach (var player in players)
        {
            if (player != minePlayerInfo)
            {
                var trs = player.GetComponent<MapTransform>();
                var state = player.GetComponent<PlayerState>();
                othersList.Add(new PlayerStateAndPos(trs, state));
            }
        }

        otherPlayers = othersList.ToArray();
    }

    public MapVec SerchOpponentDirection()
    {
        MapVec ret = new(int.MaxValue, int.MaxValue);
        int minSqrDistance = int.MaxValue;

        foreach (var player in otherPlayers)
        {
            if (player.state == minePlayer.state) continue;

            var dir = player.trs.Pos.gridPos - minePlayer.trs.Pos.gridPos;
            int sqrDistance = dir.x * dir.x + dir.y * dir.y;

            if(sqrDistance < minSqrDistance)
            {
                ret = dir;
                minSqrDistance = sqrDistance;
            }
        }

        return ret;
    }
}
