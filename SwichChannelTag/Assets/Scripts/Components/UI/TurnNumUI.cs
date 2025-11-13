using TMPro;
using UnityEngine;

public class TurnNumUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    void Update()
    {
        _text.text = "Turn: " + GameStatsManager.Instance.Turn.GetTurn().ToString();
    }
}
