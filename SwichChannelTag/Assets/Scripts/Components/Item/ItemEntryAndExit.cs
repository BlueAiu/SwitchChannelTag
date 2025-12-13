using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntryAndExit : MonoBehaviour
{
    void Start()
    {
        ItemWorldManager.AddItem(gameObject);
    }

    private void OnDestroy()
    {
        ItemWorldManager.RemoveItem(gameObject);
    }
}
