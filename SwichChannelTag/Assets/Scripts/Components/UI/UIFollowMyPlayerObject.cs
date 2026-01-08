using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//Canvas上で自分のプレイヤーに追従する機能

public class UIFollowMyPlayerObject : MonoBehaviour
{
    [SerializeField]
    RectTransform _targetUI;

    [SerializeField]
    Vector2 _offset;

    [SerializeField]
    Canvas _canvas;

    Transform _myTrs;
    RectTransform _canvasRect;

    void Awake()
    {
        _myTrs = PlayersManager.GetComponentFromMinePlayer<Transform>();
        _canvasRect = _canvas.transform as RectTransform;
    }

    void LateUpdate()
    {
        if (_targetUI == null || _myTrs == null) return;

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(_myTrs.position);

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRect,
            screenPos,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _canvas.worldCamera,
            out localPos
        );

        _targetUI.localPosition = localPos + _offset;
    }
}
