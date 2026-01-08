using TMPro;
using UnityEngine;
using UnityEngine.UI;

//作成者:杉山
//プレイヤーの状態を表示するUIの書き換えを行う機能(それぞれのプレイヤーの状態UIに付ける)

public class SetPlayerStateUI : MonoBehaviour
{
    [Tooltip("アイコンの裏の石")] [SerializeField]
    Image _stoneImage;

    [Tooltip("アイコン")] [SerializeField]
    Image _iconImage;

    [Tooltip("状態を表示するテキスト")] [SerializeField]
    TextMeshProUGUI _stateText;

    public void SetStoneUIColor(Color stoneUIColor)
    {
        _stoneImage.color = stoneUIColor;
    }

    public void SetIconSprite(Sprite iconSprite)
    {
        _iconImage.sprite = iconSprite;
    }

    public void SetStateText(string state)
    {
        _stateText.text = state;
    }
}
