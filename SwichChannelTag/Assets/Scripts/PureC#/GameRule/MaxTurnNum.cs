using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxTurnNum", menuName = "ScriptableObjects/MaxTurnNum")]
public class MaxTurnNumConfig : ScriptableObject
{
    [SerializeField] int maxTurnNum = 20;

    public int MaxTurnNum { get => maxTurnNum; }
}
