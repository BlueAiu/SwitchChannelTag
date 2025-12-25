using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//オプションのUI処理

public partial class OptionManager
{
    [System.Serializable]
    class OptionManager_UI
    {
        [SerializeField]
        EventSystem _eventSystem;

        [Tooltip("オプションメニューを開く機能")] [SerializeField]
        ShowUITypeBase _openOption;

        [Tooltip("オプションメニューを閉じる機能")] [SerializeField]
        HideUITypeBase _closeOption;

        [Tooltip("オプションメニューを閉じた時に表示したいオブジェクト")] [SerializeField]
        ShowUITypeBase _showObjectOnCloseOption;

        [Tooltip("オプションメニューを開いた時に非表示にしたいオブジェクト")] [SerializeField]
        HideUITypeBase _hideObjectOnOpenOption;

        Button _openButton;//オプションメニューを開くボタン
        Button _closeButton;//オプションメニューを閉じるボタン

        public void Awake(Button openButton,Button closeButton)
        {
            _openButton = openButton;
            _closeButton = closeButton;
        }

        public void Start()
        {
            _closeOption.Hide();//ゲーム開始時にはオプションメニューは閉じておく
        }

        public void OnOpen()
        {
            _openOption.Show();
            _hideObjectOnOpenOption.Hide();
            _eventSystem.SetSelectedGameObject(_closeButton.gameObject);//選択ボタンを閉じるボタンに設定
        }

        public void OnClose()
        {
            _closeOption.Hide();
            _showObjectOnCloseOption.Show();
            _eventSystem?.SetSelectedGameObject(_openButton.gameObject);//選択ボタンを開くボタンに設定
        }

        public void OnDisable()
        {
            if(_closeOption != null) _closeOption.Hide();
        }
    }
    
}
