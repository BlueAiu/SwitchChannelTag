using System;
using UnityEngine;

public class PlayerEntryAndExit : MonoBehaviour
{
    public event Action OnEntry;
    public event Action OnExit;

    void Start()
    {
        PlayersManager.AddPlayer(gameObject);
        OnEntry?.Invoke();
    }

    private void OnDestroy()
    {
        PlayersManager.RemovePlayer(gameObject);
        OnExit?.Invoke();
    }
}
