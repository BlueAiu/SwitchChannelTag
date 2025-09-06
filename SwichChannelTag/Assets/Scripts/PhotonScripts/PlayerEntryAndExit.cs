using UnityEngine;

public class PlayerEntryAndExit : MonoBehaviour
{
    void Start()
    {
        PlayersManager.AddPlayer(gameObject);
    }

    private void OnDestroy()
    {
        PlayersManager.RemovePlayer(gameObject);
    }
}
