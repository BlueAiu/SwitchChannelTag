using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//作成者:杉山
//クレジット画面の処理

public partial class CreditManager : MonoBehaviour
{
    [Tooltip("クレジットを開く機能")] [SerializeField]
    Button _openCreditButton;

    [Tooltip("クレジットを閉じる機能")] [SerializeField]
    Button _closeCreditButton;

    [Tooltip("UI関係")] [SerializeField]
    CreditManager_UI _uiProcess;

    public void OnOpen()//クレジットメニューを開く時
    {
        _uiProcess.OnOpen();
    }

    public void OnClose()//クレジットメニューを閉じる時
    {
        _uiProcess.OnClose();
    }

    private void Awake()
    {
        _uiProcess.Awake(_openCreditButton, _closeCreditButton);

        _openCreditButton.onClick.AddListener(OnOpen);
        _closeCreditButton.onClick.AddListener(OnClose);
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiProcess.Start();
    }
}
