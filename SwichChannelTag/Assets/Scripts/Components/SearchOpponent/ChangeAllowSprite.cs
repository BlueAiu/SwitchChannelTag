using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ChangeAllowSprite", menuName ="ScriptableObjects/ChangeAllowSprite")]
public class ChangeAllowSprite : ScriptableObject
{
    public SerializableDictionary<EPlayerState, List<Sprite>> sprites;

    [SerializeField]
    [Tooltip("‹——£‚ª¬‚³‚¢Žž‚Ìè‡’l")]
    public float smallLength = 2f;
    [SerializeField]
    [Tooltip("‹——£‚ª‘å‚«‚¢Žž‚Ìè‡’l")]
    public float lergeLength = 5f;

    const int lergeIndex = 0;
    const int mediumIndex = 1;
    const int smallIndex = 2;

    public Sprite GetAllowSprites(EPlayerState state, float sqrLength)
    {
        int index;
        if (sqrLength > lergeLength * lergeLength) { index = lergeIndex; }
        else if (sqrLength > smallLength * smallLength) { index = mediumIndex; }
        else { index = smallIndex; }

        return sprites[state][index];
    }
}
