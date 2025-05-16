using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader:MonoBehaviour
{
    // Load by build index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Load by scene name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load scene asynchronously (shows loading progress)
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private System.Collections.IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log($"Loading progress: {progress * 100}%");
            
            yield return null;
        }
    }
}