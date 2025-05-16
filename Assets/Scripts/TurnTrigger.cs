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
    [SerializeField] private float turnSpeed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("LevelGenerator")) return;
        other.GetComponent<LevelGenerator>().SetAngle(newAngle, turnSpeed * 2);
        Destroy(gameObject);
    }
}
