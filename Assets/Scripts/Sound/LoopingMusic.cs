using UnityEngine;

public class LoopingMusic : MonoBehaviour
{
    /// <summary>
    /// This object's audio source
    /// </summary>
    private AudioSource _audio;

    /// <summary>
    /// The loop points
    /// </summary>
    [SerializeField] private float startAt, loopAt;
    
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_audio.isPlaying)
        {
            // Failsafe loop
            _audio.Play(0);
            _audio.time = startAt;
            return;
        }

        // The actual loop
        if (_audio.time >= loopAt) _audio.time = startAt;
    }
}
