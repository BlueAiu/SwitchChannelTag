using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOverlapPlayer : MonoBehaviour
{
    [SerializeField] bool writeLog = false;

    MapTransform minePlayer;
    MapTransform[] otherPlayers;

    // Start is called before the first frame update
    void Start()
    {
        minePlayer = PlayersManager.GetComponentFromMinePlayer<MapTransform>();

        var players = PlayersManager.GetComponentsFromPlayers<MapTransform>();
        List<MapTransform> othersList = new();

        foreach (var player in players)
        {
            if (player != minePlayer)
            {
                othersList.Add(player);
            }
        }

        otherPlayers = othersList.ToArray();
    }

    public GameObject[] GetOverlapPlayers()
    {
        List<GameObject> overlapPlayers = new();
        foreach (var other in otherPlayers)
        {
            // MapTransformに==オーバーロードを実装しても良い
            if (minePlayer.Pos==other.Pos)
            {
                overlapPlayers.Add(other.gameObject);
            }
        }

        if (writeLog) Log(overlapPlayers);
        return overlapPlayers.ToArray();
    }
    
    void Log(List<GameObject> objects)
    {
        string log = "GetOverlapPlayers: ";
        foreach (var obj in objects)
        {
            log += obj.GetComponent<PlayerNumber>().PlayerNum + ", ";
        }
        Debug.Log(log);
    }
}
