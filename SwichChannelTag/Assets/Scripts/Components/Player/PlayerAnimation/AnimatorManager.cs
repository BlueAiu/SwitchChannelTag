using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, Animator> _animator;
    [SerializeField] PlayerState _state;

    Animator CurAnim { get => _animator[_state.State]; }

    const string captureTrigger = "CaptureTrigger";
    const string isMoving = "IsMoving";
    const string switchTrigger = "SwitchTrigger";
    

    public void StartMove()
    {
        CurAnim.SetBool(isMoving, true);
    }

    public void EndMove()
    {
        CurAnim.SetBool(isMoving, false);
    }

    public void CaptureTrigger()
    {
        CurAnim.SetTrigger(captureTrigger);
    }

    public void SwitchTrigger()
    {
        CurAnim.SetTrigger(switchTrigger);
    }
}
