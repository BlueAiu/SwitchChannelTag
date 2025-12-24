using Photon.Pun;
using TMPro;
using UnityEngine;

//プレイヤー名を表示する機能
//SetNameTextで表示名を変更
//このコンポーネントのアクティブ状態を切り替えれば、名前の表示・非表示を変更可能

public class ShowPlayerNameTag : MonoBehaviour
{
    [SerializeField]
    GameObject _nameTag;

    [SerializeField] 
    TextMeshProUGUI _nameText;

    private void OnEnable()
    {
        _nameTag.SetActive(true);
    }

    private void OnDisable()
    {
        _nameTag.SetActive(false);
    }

    public void SetNameText(string newName)
    {
        if (_nameText.text == newName) return;

        _nameText.text = newName;
    }
}
