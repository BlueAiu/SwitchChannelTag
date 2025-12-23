using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//自分のプレイヤーの色を変える

public class ChangeMyColor : MonoBehaviour
{
    [Tooltip("鬼のスキン")] [SerializeField]
    Renderer _taggerSkin;

    [Tooltip("逃げのスキン")] [SerializeField] 
    Renderer _runnerSkin;

    Material _taggerMat;
    Material _runnerMat;

    private void Awake()
    {
        _taggerMat = _taggerSkin.material;
        _runnerMat = _runnerSkin.material;
    }

    public void SetColor(Color color)
    {
        //鬼の色を変更
        _taggerMat.color = color;

        //逃げの色を変更
        _runnerMat.color = color;
        _runnerMat.SetColor("_EmissionColor", color);
    }
}
