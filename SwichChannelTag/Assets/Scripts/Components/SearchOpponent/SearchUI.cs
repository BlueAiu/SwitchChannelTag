using System.Collections.Generic;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField] SearchOpponentPlayer search;
    [SerializeField] RectTransform uiTrs;
    [SerializeField] UnityEngine.UI.Image uiImage;
    [SerializeField] SerializableDictionary<EPlayerState, List<Sprite>> sprites;

    [SerializeField]
    [Tooltip("距離が小さい時の閾値")] 
    float smallLength = 2f;
    [SerializeField]
    [Tooltip("距離が大きい時の閾値")]
    float lergeLength = 5f;
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

        int index;
        if (sqrDistance > lergeLength * lergeLength) { index = lergeIndex; }
        else if (sqrDistance > smallLength * smallLength) { index = mediumIndex; }
        else { index = smallIndex; }

        uiImage.sprite = sprites[mineState.State][index];

        var angle = Mathf.Atan2(-direction.x, -direction.y);
        angle *= Mathf.Rad2Deg;
        angle = MathfExtension.RoundByAlpha(angle, OmniAngle / angleDivision);
        uiTrs.eulerAngles = new Vector3(0, 0, angle);
    }
}
