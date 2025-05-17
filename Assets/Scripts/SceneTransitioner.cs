using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    private bool _isLoading = false;

    private Animator _animator;

    private int _sceneToLoad;

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
        _animator.SetTrigger("EndLoading");
    }
    
    /// <summary>
    /// Gets called by the animator
    /// </summary>
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
