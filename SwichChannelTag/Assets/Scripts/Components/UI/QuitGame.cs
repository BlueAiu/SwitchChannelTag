using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームを終了する

public class QuitGame : MonoBehaviour
{
    [SerializeField]
    float _waitTime = 1f;

    bool _requestQuit = false;

    public void Quit()
    {
        if (_requestQuit) return;

        _requestQuit = true;
        StartCoroutine(QuitCoroutine());
    }

    IEnumerator QuitCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
