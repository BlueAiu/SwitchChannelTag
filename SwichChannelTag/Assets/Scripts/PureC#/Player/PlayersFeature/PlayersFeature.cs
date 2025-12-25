using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤー番号ごとの名前や色

[CreateAssetMenu(fileName = "PlayersFeature", menuName = "ScriptableObjects/PlayersFeature")]
public class PlayersFeature : ScriptableObject
{
    [System.Serializable]
    public struct Feature
    {
        public string name;
        public Color color;
    }

    [Tooltip("プレイヤー番号ごとのプレイヤーの特徴\nロビーに入った順に0番の要素から順に割り当てられていく")] [SerializeField]
    Feature[] _features;

    public Feature[] Features { get { return _features; } }
}
