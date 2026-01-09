using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//作成者:杉山
//プレイヤー番号ごとの名前や色

[CreateAssetMenu(fileName = "PlayersFeature", menuName = "ScriptableObjects/PlayersFeature")]
public class PlayersFeature : ScriptableObject
{
    [System.Serializable]
    public struct Feature
    {
        [Tooltip("プレイヤー名")] 
        public string name;

        [Tooltip("プレイヤーモデルの色")]
        public Color playerModelColor;

        [Tooltip("幽霊モデルのマテリアル")]
        public Material[] runnerModelMaterials;

        [Tooltip("鬼モデルのマテリアル")]
        public Material[] taggerModelMaterials;

        [Tooltip("プレイヤーの状態UIに表示する色")]
        public Color stateUIColor;
    }

    [Tooltip("プレイヤー番号ごとのプレイヤーの特徴\nロビーに入った順に0番の要素から順に割り当てられていく")] [SerializeField]
    Feature[] _features;

    public Feature[] Features { get { return _features; } }
}
