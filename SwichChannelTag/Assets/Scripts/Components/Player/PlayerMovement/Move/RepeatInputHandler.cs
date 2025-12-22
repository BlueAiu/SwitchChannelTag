using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//単発とリピート入力を検知する機能(ボタンの入力とスティックのような2次元の値を取得するものに対応)

public class RepeatInputHandler : MonoBehaviour
{
    [SerializeField] float _repeatWaitDuration;

    bool _isWaiting;
    bool _isInputting;
    Coroutine _waitCoroutine;

    Vector2 _contextVec;

    public event Action<Vector2> OnInputVec2;
    public event Action OnInput;
    public event Action OnCancel;

    public void Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isInputting = true;
            _contextVec = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            OnCancel?.Invoke();

            _isInputting = false;

            if (_waitCoroutine != null) StopCoroutine(_waitCoroutine);
            
            CancelWait();
        }
    }

    void Update()
    {
        if (_isInputting)
        {
            TryInput();
        }
    }

    void TryInput()
    {
        if (_isWaiting) return;

        // 入力があったとして処理
        OnInput?.Invoke();
        OnInputVec2?.Invoke(_contextVec);

        _isWaiting = true;
        _waitCoroutine = StartCoroutine(InputWaitCoroutine());
    }

    IEnumerator InputWaitCoroutine()
    {
        yield return new WaitForSeconds(_repeatWaitDuration);
        CancelWait();
    }

    void CancelWait()//待ち状態を解除
    {
        _isWaiting = false;
        _waitCoroutine = null;
    }
}