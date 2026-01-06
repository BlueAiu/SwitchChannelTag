using TMPro;
using UnityEngine;

public class TurnNumUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] MaxTurnNumConfig _maxTurn;

    void FixtdUpdate()
    {
        _text.text = (_maxTurn.MaxTurnNum - GameStatsManager.Instance.Turn.GetTurn() + 1).ToString();
    }
}
