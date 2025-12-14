using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOverLapItems : MonoBehaviour
{
    MapTransform player;

    private void Start()
    {
        player = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }

    public GameObject[] GetItems()
    {
        var items = ItemWorldManager.GetComponentsItems<MapTransform>();

        List<GameObject> ret = new();

        foreach ( var item in items)
        {
            if(item.Pos == player.Pos)
            {
                ret.Add(item.gameObject);
            }
        }

        return ret.ToArray();
    }
}
