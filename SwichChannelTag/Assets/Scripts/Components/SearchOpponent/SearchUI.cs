using System.Collections.Generic;
using UnityEngine;

public class SearchUI : MonoBehaviour
{
    [SerializeField]
    SearchOpponentPlayer search;
    
    [SerializeField] 
    ChangeCompassEffectByDistanceAndState _changeCompassEffect;

    [SerializeField]
    RectTransform _compassUICenter;
    
    [SerializeField] [Tooltip("矢印を出さない時の閾値")]
    float searchRange = 8f;

    [SerializeField] [Tooltip("矢印の向きを分割する数（4の倍数推奨）")]
    int angleDivision = 8;

    PlayerState mineState;

    const float OmniAngle = 360f;


    private void Awake()
    {
        mineState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }

    private void FixedUpdate()
    {
        SetCompass();
    }

    public void SetCompass()
    {
        var direction = search.SerchOpponentDirection();
        float distance = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);

        if (distance > searchRange)
        {
            _changeCompassEffect.HideAllEffect();
            return;
        }

        _changeCompassEffect.RefleshEffect(mineState.State, distance);

        if (distance < float.Epsilon)
        {
            _compassUICenter.eulerAngles = Vector3.zero;
        }
        else
        {
            var angle = Mathf.Atan2(-direction.x, -direction.y);
            angle *= Mathf.Rad2Deg;
            angle = MathfExtension.RoundByAlpha(angle, OmniAngle / angleDivision);
            _compassUICenter.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
