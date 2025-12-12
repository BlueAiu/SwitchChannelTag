using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class ItemWorldManager
{
    public static List<GameObject> itemList = new();


    // --- Getter --- //

    public static GameObject[] ItemsGameObject
    {
        get
        {
            List<GameObject> ret = new();

            foreach (var i in itemList)
            {
                if (i == null) continue;

                ret.Add(i);
            }

            return ret.ToArray();
        }
    }

    public static T[] GetComponentsItems<T>() where T : Component
    {

        List<T> ret = new();

        foreach (var i in itemList)
        {
            if (i == null) continue;

            T comp = i.GetComponent<T>();

            if (comp != null) { ret.Add(comp); }
        }

        return ret.ToArray();
    }


    // --- Add & Remove --- //

    public static void AddItem(GameObject item)
    {
        if (itemList.Contains(item)) return;
        itemList.Add(item);
        //SortByInstanceID();
    }

    public static void RemoveItem(GameObject item)
    {
        if (item == null) return;
        if (!itemList.Contains(item )) return;
        itemList.Remove(item);
        //SortByInstanceID();
    }

    static void SortByInstanceID()
    {
        itemList.Sort((a, b) =>
        a.GetInstanceID().CompareTo(
            b.GetInstanceID()));
    }
}
