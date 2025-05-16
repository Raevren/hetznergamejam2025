using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    /// The time until the next level piece spawns
    /// </summary>
    private float _distanceUntilNext;
    private float _currentRotation, _turnSpeed;
    
    [SerializeField] private float zToSpawnAt, zToDespawnAt;
    [SerializeField] private PlayerSpeed speedScript;
    
    [SerializeField] private Transform levelParent;
    /// <summary>
    /// All level prefabs
    /// </summary>
    private GameObject[] _levelPrefabs;
    /// <summary>
    /// All currently instantiated levels
    /// </summary>
    [SerializeField] private List<GameObject> instantiatedLevels;
    
    private void Start()
    {
        LoadElements();
    }

    private void Update()
    {
        MoveLevels();
        CheckDistance();
        Rotate();
    }

    /// <summary>
    /// Move all level pieces currently instantiated
    /// </summary>
    private void MoveLevels()
    {
        var levelsToDespawn = new List<GameObject>();
        
        // Moving
        foreach (var level in instantiatedLevels)
        {
            level.transform.Translate(Vector3.back * (speedScript.Speed * Time.deltaTime));
            // Check if the level should despawn
            if (level.transform.localPosition.z <= zToDespawnAt) levelsToDespawn.Add(level);
        }
        
        // Despawning
        foreach (var level in levelsToDespawn)
        {
            instantiatedLevels.Remove(level);
            Destroy(level);
        }
    }

    /// <summary>
    /// Rotate all levels to the current rotation
    /// </summary>
    private void Rotate()
    {
        levelParent.rotation = Quaternion.RotateTowards(levelParent.rotation, Quaternion.Euler(0, _currentRotation, 0), Time.deltaTime * speedScript.Speed * _turnSpeed);
    }

    /// <summary>
    /// Wait for the next level piece to spawn
    /// </summary>
    private void CheckDistance()
    {
        if (_distanceUntilNext > 0)
        {
            // Decrease the distance by the current player speed
            _distanceUntilNext -= Time.deltaTime * speedScript.Speed;
            return;
        }
        // Spawn the next level piece
        SpawnElement();
        _distanceUntilNext = zToSpawnAt;
    }

    /// <summary>
    /// Loads all level prefabs from the resources
    /// </summary>
    private void LoadElements()
    {
        _levelPrefabs = Resources.LoadAll<GameObject>("Prefabs/LevelElements");
    }
    
    /// <summary>
    /// Spawns the next level piece
    /// </summary>
    private void SpawnElement()
    {
        var level = Instantiate(_levelPrefabs[Random.Range(0, _levelPrefabs.Length)], levelParent);
        level.transform.Translate(Vector3.forward * zToSpawnAt);
        instantiatedLevels.Add(level);
    }

    public void SetAngle(float newAngle, float turnSpeed)
    {
        _currentRotation = newAngle;
        _turnSpeed = Mathf.Max(0.1f, turnSpeed);
    }
}
