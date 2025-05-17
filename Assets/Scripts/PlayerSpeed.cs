using QuickTimeEvents;
using TMPro;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public float Speed { get; private set; } = 1;

    // 0 no, -1 left, 1 right
    private int _previosPres = 0;

    private float _lastPressedTime = 0;

    [SerializeField] private float increasePerPress = 0.1f;
    [SerializeField] private float slowDownThreshold = 0.00001f;

    [SerializeField] private Transform balancePivot; 
    [SerializeField] private QuickTimeEventManager quickTimeEventManager;

    private void Update()
    {
        if (quickTimeEventManager.IsActive)
        {
            return;
        }
        
        UpdateSpeedDecrease();
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        if(_previosPres == 0) return;


        var angle = 0;
        var zDirection = transform.rotation.z;
        if (zDirection < 0)
        {
            angle = -1;
        }else if (zDirection > 0)
        {
            angle = 1;
        }
        
        
        transform.RotateAround(balancePivot.position, new Vector3(0,0,1), 5 * Time.deltaTime * angle);
    }

    private void UpdateSpeedDecrease()
    {
        if (Speed < 0)
        {
            return;
        }

        var slowDownAt = _lastPressedTime + slowDownThreshold;
        if ((slowDownAt > Time.time))
        {
            return;
        }

        Speed -= increasePerPress * Time.deltaTime;
    }

    private void OnBalanceLeft()
    {
        TryIncreaseSpeed(-1);
    }

    private void OnBalanceRight()
    {
        TryIncreaseSpeed(1);
    }

    private void TryIncreaseSpeed(int pressedButton)
    {
        _lastPressedTime = Time.time;
        if (_previosPres != pressedButton)
        {
            Speed += increasePerPress;
        }
        _previosPres = pressedButton;
        
        transform.RotateAround(balancePivot.position, new Vector3(0,0,1), -10 *  _previosPres);
    }
}
