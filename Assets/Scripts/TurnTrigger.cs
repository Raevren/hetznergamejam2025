using System.Collections;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    /// <summary>
    /// The angle to turn to
    /// </summary>
    [SerializeField] private float newAngle;
    
    /// <summary>
    /// The speed to turn the levels with
    /// </summary>
    [SerializeField] private float turnRadius;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("LevelGenerator")) return;
        var levelParent = GameObject.FindWithTag("LevelParent");
        levelParent.GetComponent<LevelRotator>().Rotate(newAngle, turnRadius);
        Destroy(gameObject);
    }
}
