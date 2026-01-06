using TMPro;
using UnityEngine;

public class TurnNumUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] MaxTurnNumConfig _maxTurn;

    private void FixedUpdate()
    {
        _text.text = (_maxTurn.MaxTurnNum - GameStatsManager.Instance.Turn.GetTurn() + 1).ToString();
    }
}
