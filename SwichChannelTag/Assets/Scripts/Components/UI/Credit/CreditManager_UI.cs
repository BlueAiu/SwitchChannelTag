using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

//作成者:杉山
//クレジットのUI処理

public partial class CreditManager
{
    [System.Serializable]
    class CreditManager_UI
    {
        [SerializeField]
        EventSystem _eventSystem;

        [Tooltip("クレジットメニューを開く機能")] [SerializeField]
        ShowUITypeBase _openCredit;

        [Tooltip("クレジットメニューを閉じる機能")] [SerializeField]
        HideUITypeBase _closeCredit;

        [Tooltip("クレジットメニューを閉じた時に表示したいオブジェクト")] [SerializeField]
        ShowUITypeBase _showObjectOnCloseCredit;

        [Tooltip("クレジットメニューを開いた時に非表示にしたいオブジェクト")] [SerializeField]
        HideUITypeBase _hideObjectOnOpenCredit;

        Button _openButton;//クレジットメニューを開くボタン
        Button _closeButton;//クレジットメニューを閉じるボタン

        public void Awake(Button openButton, Button closeButton)
        {
            _openButton = openButton;
            _closeButton = closeButton;
        }

        public void Start()
        {
            _closeCredit.Hide();//ゲーム開始時にはクレジットメニューを閉じておく
        }

        public void OnOpen()
        {
            _openCredit.Show();
            _hideObjectOnOpenCredit.Hide();
            _eventSystem.SetSelectedGameObject(_closeButton.gameObject);//選択ボタンを閉じるボタンに設定
        }

        public void OnClose()
        {
            _closeCredit.Hide();
            _showObjectOnCloseCredit.Show();
            _eventSystem?.SetSelectedGameObject(_openButton.gameObject);//選択ボタンを開くボタンに設定
        }
    }
}
