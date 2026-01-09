using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] string canvasPath = "PlayerCanvas";

    void Start()
    {
        var players = PlayersManager.PlayersGameObject;

        foreach (var p in players)
        {
            var canvas = p.transform.Find(canvasPath);
            if (canvas != null) canvas.gameObject.SetActive(false);
        }
    }
}
