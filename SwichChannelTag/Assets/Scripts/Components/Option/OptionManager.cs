using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//作成者:杉山
//オプション画面の処理

public partial class OptionManager : MonoBehaviour
{
    [Tooltip("オプションを開くボタン")] [SerializeField]
    Button _openOptionButton;

    [Tooltip("オプションを閉じるボタン")] [SerializeField]
    Button _closeOptionButton;

    [Tooltip("UI関係")] [SerializeField]
    OptionManager_UI _uiProcess;

    public void OnOpen()//オプションメニューを開く時
    {
        _uiProcess.OnOpen();
    }

    public void OnClose()//オプションメニューを閉じる時
    {
        _uiProcess.OnClose();
    }



    private void OnDisable()
    {
        _uiProcess.OnDisable();
    }

    private void Awake()
    {
        _uiProcess.Awake(_openOptionButton,_closeOptionButton);

        _openOptionButton.onClick.AddListener(OnOpen);
        _closeOptionButton.onClick.AddListener(OnClose);
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiProcess.Start();
    }
}
