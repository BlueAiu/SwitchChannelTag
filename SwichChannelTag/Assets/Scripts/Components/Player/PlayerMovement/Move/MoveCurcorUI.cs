using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//à⁄ìÆÉJÅ[É\Éã

public class MoveCurcorUI : MonoBehaviour
{
    [SerializeField] GameObject _cursorPrefab;
    GameObject _cursor;

    public void MoveCursor(Vector3 cursorPos)
    {
        if (_cursor == null) return;

        _cursor.transform.position = cursorPos;
    }

    public void OnStartDecide(Vector3 cursorPos)
    {
        _cursor = Instantiate(_cursorPrefab, cursorPos, Quaternion.identity);
    }

    public void OnFinishDecide()
    {
        Destroy(_cursor);
        _cursor = null;
    }
}
