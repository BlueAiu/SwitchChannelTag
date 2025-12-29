using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] string spritePath = "PlayerCanvas/CompassSprite";

    void Start()
    {
        var compassSprite = PlayersManager.MinePlayerGameObject.transform.Find(spritePath);
        compassSprite.GetComponent<UnityEngine.UI.Image>().enabled = false;
    }
}
