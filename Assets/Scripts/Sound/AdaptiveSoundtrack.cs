using UnityEngine;

public class AdaptiveSoundtrack : MonoBehaviour
{
    [SerializeField] private AudioSource main, instrumental;

    [SerializeField] private PlayerSpeed speedScript;
    
    private void Update()
    {
        var audioStrength = speedScript.Speed / speedScript.MaxSpeed;
        main.volume = audioStrength;
        instrumental.volume = 1f - audioStrength;
    }
}
