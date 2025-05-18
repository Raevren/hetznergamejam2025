using QuickTimeEvents;
using TMPro;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public float Speed { get; private set; } = 1;

    [field: SerializeField] public float MaxSpeed { get; private set; } = 4;

    // 0 no, -1 left, 1 right
    private int _previousPress = 0;

    private float _lastPressedTime = 0;

    private float score = 0;
    
    [SerializeField] private float increasePerPress = 0.1f;
    [SerializeField] private float slowDownThreshold = 0.00001f, decreaseSpeed = 0.3f;

    [SerializeField] private Transform balancePivot;
    [SerializeField] private QuickTimeEventManager quickTimeEventManager;
    private PlayerHealth _playerHealth;

    [SerializeField] private SpriteRenderer bearRenderer;
    [SerializeField] private TMP_Text speedText; 
    [SerializeField] private Sprite spriteLeft, spriteRight;

    /// <summary>
    /// The AudioSource playing sound effects for this bear
    /// </summary>
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip wheelLeftSfx, wheelRightSfx;
    
    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();

        _playerHealth.OnRemoveHealth += () => Speed = 0;
    }

    private void Update()
    {
        if(!_playerHealth.IsAllowedToMove) return;
        
        var deltaScore = Time.deltaTime * (Speed / 50);
        score += deltaScore;
        speedText.text = "Entfernung: " + (Mathf.Round(score * 100) / 100) + "M";
        if (quickTimeEventManager.IsActive)
        {
            return;
        }
        
        UpdateSpeedDecrease();
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        if(_previousPress == 0) return;


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

        Speed -= decreaseSpeed * Time.deltaTime;
    }

    public void OnBalanceLeft()
    {
        PlaySound(false);
        TryIncreaseSpeed(-1);
    }

    public void OnBalanceRight()
    {
        PlaySound(true);
        TryIncreaseSpeed(1);
    }

    private void TryIncreaseSpeed(int pressedButton)
    {
        _lastPressedTime = Time.time;
        if (_previousPress != pressedButton)
        {
            Speed += increasePerPress;
            Speed = Mathf.Min(Speed, MaxSpeed);
        }
        _previousPress = pressedButton;
        bearRenderer.sprite = pressedButton < 0 ? spriteLeft : spriteRight;

        transform.RotateAround(balancePivot.position, new Vector3(0,0,1), -10 *  _previousPress);
    }

    /// <summary>
    /// Plays the wheel sound
    /// </summary>
    private void PlaySound(bool left)
    {
        if (sfxSource.isPlaying) return;
        sfxSource.clip = left ? wheelLeftSfx : wheelRightSfx;
        sfxSource.pitch = Random.Range(0.7f, 1.3f);
        sfxSource.Play(0);
    }
}
