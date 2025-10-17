using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ChangeAllowSprite", menuName ="ScriptableObjects/ChangeAllowSprite")]
public class ChangeAllowSprite : ScriptableObject
{
    public SerializableDictionary<EPlayerState, List<Sprite>> sprites;

    [SerializeField]
    [Tooltip("‹——£‚ª¬‚³‚¢Žž‚Ìè‡’l")]
    public float nearDistance = 2f;
    [SerializeField]
    [Tooltip("‹——£‚ª‘å‚«‚¢Žž‚Ìè‡’l")]
    public float farDistance = 5f;

    const int farIndex = 0;
    const int mediumIndex = 1;
    const int nearIndex = 2;
    const int overLapIndex = 3;

    public Sprite GetAllowSprites(EPlayerState state, float distance)
    {
        int index;
        if (distance > farDistance) { index = farIndex; }
        else if (distance > nearDistance) { index = mediumIndex; }
        else if (distance > float.Epsilon) { index = nearIndex; }
        else { index = overLapIndex; }

        return sprites[state][index];
    }
}
