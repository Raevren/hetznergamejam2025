using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private float _currentRotation, _turnSpeed;
    
    [SerializeField] private float zToDespawnAt;
    [SerializeField] private PlayerSpeed speedScript;

    /// <summary>
    /// The maximum distance of the next level spawn point until the next element spawns
    /// </summary>
    [SerializeField] private float spawnPointDistance;
    
    [SerializeField] private Transform levelParent;
    /// <summary>
    /// All level prefabs
    /// </summary>
    private GameObject[] _levelPrefabs;
    /// <summary>
    /// All currently instantiated levels
    /// </summary>
    [SerializeField] private List<GameObject> instantiatedLevels;

    /// <summary>
    /// The next level spawn point of the last instantiated level
    /// </summary>
    private Transform _currentNextSpawn;
    
    private void Start()
    {
        LoadElements();
        LoadLevelPoints();
    }

    private void Update()
    {
        MoveLevels();
        CheckDistance();
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
            level.transform.position += Vector3.back * (speedScript.Speed * Time.deltaTime);
            // Check if the level should despawn
            if (level.transform.position.z <= zToDespawnAt) levelsToDespawn.Add(level);
        }
        
        // Despawning
        foreach (var level in levelsToDespawn)
        {
            instantiatedLevels.Remove(level);
            Destroy(level);
        }
    }

    /// <summary>
    /// Wait for the next level piece to spawn
    /// </summary>
    private void CheckDistance()
    {
        // Check if the next element can spawn
        if (Vector3.Distance(transform.position, _currentNextSpawn.position) > spawnPointDistance) return;
        // Spawn the next level piece
        SpawnElement();
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
        var level = Instantiate(_levelPrefabs[Random.Range(0, _levelPrefabs.Length)], _currentNextSpawn.position, _currentNextSpawn.rotation, levelParent);
        instantiatedLevels.Add(level);
        LoadLevelPoints();
    }
    
    private void LoadLevelPoints()
    {
        var level = instantiatedLevels[^1].transform;
        _currentNextSpawn = level.Find("NextSpawn");
    }
}
