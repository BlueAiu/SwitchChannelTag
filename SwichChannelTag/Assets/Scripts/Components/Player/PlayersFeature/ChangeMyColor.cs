using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//自分のプレイヤーの色を変える

public class ChangeMyColor : MonoBehaviour
{
    [Tooltip("鬼のスキン")] [SerializeField]
    Renderer[] _taggerSkins;

    [Tooltip("逃げのスキン")] [SerializeField]
    Renderer[] _runnerSkins;

    Material[] _taggerMats;
    Material[] _runnerMats;

    private void Awake()
    {
        _taggerMats = new Material[_taggerSkins.Length];
        for (int i = 0; i < _taggerSkins.Length; i++)
        {
            _taggerMats[i] = _taggerSkins[i].material;
        }

        _runnerMats = new Material[_runnerSkins.Length];
        for (int i = 0; i < _runnerSkins.Length; i++)
        {
            _runnerMats[i] = _runnerSkins[i].material;
        }
    }

    public void SetColor(Color color)
    {
        // 鬼の色を変更
        foreach (var mat in _taggerMats)
        {
            mat.color = color;
        }

        // 逃げの色を変更
        foreach (var mat in _runnerMats)
        {
            mat.color = color;
            mat.SetColor("_EmissionColor", color);
        }
    }
}
