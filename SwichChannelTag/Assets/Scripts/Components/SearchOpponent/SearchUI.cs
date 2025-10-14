using System.Collections.Generic;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField] SearchOpponentPlayer search;
    [SerializeField] RectTransform uiTrs;
    [SerializeField] UnityEngine.UI.Image uiImage;
    [SerializeField] ChangeAllowSprite sprites;

    
    [SerializeField]
    [Tooltip("矢印を出さない時の閾値")]
    float searchRange = 8f;
    [SerializeField]
    [Tooltip("矢印の向きを分割する数（4の倍数推奨）")]
    int angleDivision = 8;

    PlayerState mineState;
    const int lergeIndex = 0;
    const int mediumIndex = 1;
    const int smallIndex = 2;

    const float OmniAngle = 360f;


    private void Awake()
    {
        mineState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }


    private void Update()
    {
        SetAllow();
    }

    public void SetAllow()
    {
        var direction = search.SerchOpponentDirection();
        float sqrDistance = direction.x * direction.x + direction.y * direction.y;

        if (sqrDistance > searchRange * searchRange)
        {
            uiImage.enabled = false;
            return;
        }
        else { uiImage.enabled = true; }

        uiImage.sprite = sprites.GetAllowSprites(mineState.State, sqrDistance);

        var angle = Mathf.Atan2(-direction.x, -direction.y);
        angle *= Mathf.Rad2Deg;
        angle = MathfExtension.RoundByAlpha(angle, OmniAngle / angleDivision);
        uiTrs.eulerAngles = new Vector3(0, 0, angle);
    }
}
