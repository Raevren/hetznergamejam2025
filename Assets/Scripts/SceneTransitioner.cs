using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    private bool _isLoading = false;

    private Animator _animator;

    private int _sceneToLoad;

    /// <summary>
    /// The parent audio mixer group for smooth fading
    /// </summary>
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Wait a couple of seconds before loading the wanted scene
    /// </summary>
    public IEnumerator LoadSceneIn(float seconds, int scene)
    {
        yield return new WaitForSeconds(seconds);
        StartLoading(scene);
    }
    
    /// <summary>
    /// Start the loading process while playing an animation
    /// </summary>
    public void StartLoading(int sceneIndex)
    {
        if (_isLoading) return;
        _isLoading = true;
        
        _sceneToLoad = sceneIndex;
        _animator.SetTrigger("StartLoading");
        StartCoroutine(FadeAudio(-40f, 0.75f));
    }

    /// <summary>
    /// Gets called by the animator
    /// </summary>
    public void ActuallyLoadScene()
    {
        if (!_isLoading) return;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(_sceneToLoad);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // The scene has loaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StartCoroutine(FadeAudio(0f, 0.75f));
        _animator.SetTrigger("EndLoading");
    }
    
    /// <summary>
    /// Gets called by the animator
    /// </summary>
    public void DestroyObject()
    {
        StopAllCoroutines();
        audioMixer.SetFloat("fadeVolume", 0f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Fade the master audio level to a wanted db level over time
    /// </summary>
    private IEnumerator FadeAudio(float wantedDB, float howLong)
    {
        audioMixer.GetFloat("fadeVolume", out var startingVolume);
        var timePassed = 0f;
        while (timePassed < howLong)
        {
            timePassed += Time.deltaTime;
            audioMixer.SetFloat("fadeVolume", Mathf.Lerp(startingVolume, wantedDB, timePassed / howLong));
            yield return new WaitForEndOfFrame();
        }
        audioMixer.SetFloat("fadeVolume", wantedDB);
    }
}
