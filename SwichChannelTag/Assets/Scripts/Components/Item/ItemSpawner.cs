using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    struct hierarchyInfo
    {
        public int index;
        public int playerCount;
        public int itemCount;

        public hierarchyInfo(int _index = 0)
        {
            index = _index;
            playerCount = 0;
            itemCount = 0;
        }
    }

    readonly MapVec[] aroundVecs = { MapVec.Zero, MapVec.Up, MapVec.Down, MapVec.Left, MapVec.Right };




    [SerializeField] GameObject _spawnItem;
    [SerializeField] Maps_Hierarchies _hierarchies;
    [SerializeField] int spawnNum = 2;

    Player mine;
    MapTransform[] players, items;

    private void Start()
    {
        mine = PlayersManager.MinePlayerPhotonPlayer;
        players = PlayersManager.GetComponentsFromPlayers<MapTransform>();
    }

    public void SpawnItems()
    {
        if (!mine.IsMasterClient) return;   // MasterClient only
        for (int i = 0; i < spawnNum; i++) SpawnItemPrefab();
    }

    void SpawnItemPrefab()
    {
        // Initialize MapInfo

        items = ItemWorldManager.GetComponentsItems<MapTransform>();

        hierarchyInfo[] hierarchiesInfo = new hierarchyInfo[_hierarchies.Length];
        List<MapVec>[] objVecs = new List<MapVec>[_hierarchies.Length];

        for (int i = 0; i < _hierarchies.Length; i++) 
        {
            hierarchiesInfo[i] = new hierarchyInfo(i);
            objVecs[i] = new List<MapVec>();
        }

        foreach(var item in items)
        {
            var pos = item.Pos;
            hierarchiesInfo[pos.hierarchyIndex].itemCount++;
            objVecs[pos.hierarchyIndex].Add(pos.gridPos);
        }

        foreach(var p in players)
        {
            var pos = p.Pos;
            hierarchiesInfo[pos.hierarchyIndex].playerCount++;
            objVecs[pos.hierarchyIndex].Add(pos.gridPos);
        }

        int spawnIndex = SelectHierarchy(hierarchiesInfo);

        var spawnGridPos = SelectGridPos(objVecs[spawnIndex].ToArray());

        InstantiateItem(new MapPos(spawnIndex, spawnGridPos));
    }

    void InitializeMapInfo()
    {
        items = ItemWorldManager.GetComponentsItems<MapTransform>();

        hierarchyInfo[] hierarchiesInfo = new hierarchyInfo[_hierarchies.Length];
        List<MapVec>[] objVecs = new List<MapVec>[_hierarchies.Length];

        for (int i = 0; i < _hierarchies.Length; i++)
        {
            hierarchiesInfo[i] = new hierarchyInfo(i);
            objVecs[i] = new List<MapVec>();
        }

        foreach (var item in items)
        {
            var pos = item.Pos;
            hierarchiesInfo[pos.hierarchyIndex].itemCount++;
            objVecs[pos.hierarchyIndex].Add(pos.gridPos);
        }

        foreach (var p in players)
        {
            var pos = p.Pos;
            hierarchiesInfo[pos.hierarchyIndex].playerCount++;
            objVecs[pos.hierarchyIndex].Add(pos.gridPos);
        }
    }

    int SelectHierarchy(hierarchyInfo[] hierarchiesInfo)
    {
        hierarchiesInfo = hierarchiesInfo
           .OrderBy(h => h.itemCount)
           .ThenBy(h => h.playerCount)
           .ThenBy(h => UnityEngine.Random.value) // “¯’l‚Ì’†‚Åƒ‰ƒ“ƒ_ƒ€
           .ToArray();

        return hierarchiesInfo[0].index;
    }

    MapVec SelectGridPos(MapVec[] objVecs)
    {
        List<MapVec> mapVecs = new();
        for (int i = 0; i < _hierarchies[0].MapSize_X; i++)
        {
            for (int j = 0; j < _hierarchies[0].MapSize_Y; j++)
            {
                mapVecs.Add(new MapVec(i, j));
            }
        }

        foreach (var vec in objVecs)
        {
            foreach (var v in aroundVecs)
            {
                var p = vec + v;
                mapVecs.Remove(p);
            }
        }

        return mapVecs[UnityEngine.Random.Range(0, mapVecs.Count)];
    }

    void InstantiateItem(MapPos pos)
    {
        var item_i = PhotonNetwork.Instantiate(_spawnItem.name, Vector3.zero, Quaternion.identity);
        var item_t = item_i.GetComponent<MapTransform>();
        item_t.Hierarchies = _hierarchies;
        item_t.Rewrite(pos);
        item_i.GetComponent<SetTransform>().Position = item_t.CurrentWorldPos;
    }

}
