using UnityEngine;

public class GameDebugging : MonoBehaviour
{
    void Start()
    {
        #if !DEBUG && !UNITY_EDITOR
        Destroy(gameObject);
        return;
        #endif
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad5)) Debug.Log("Test");
    }
}
