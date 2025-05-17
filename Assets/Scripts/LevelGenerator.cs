using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
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

    /// <summary>
    /// The next level spawn point of the last instantiated level
    /// </summary>
    private Transform _currentNextSpawn;
    
    private void Start()
    {
        LoadElements();
        SetSpawnPoint();
    }

    private void Update()
    {
        MoveLevels();
    }

    /// <summary>
    /// Move all level pieces currently instantiated
    /// </summary>
    private void MoveLevels()
    {
        // Moving
        foreach (var level in instantiatedLevels)
        {
            level.transform.position += Vector3.back * (speedScript.Speed * Time.deltaTime);
        }
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
        SetSpawnPoint();

        // Check if a level needs to be destroyed
        if (instantiatedLevels.Count < 3) return;
        // Destroy the oldest level
        var remove = instantiatedLevels[0];
        instantiatedLevels.RemoveAt(0);
        Destroy(remove.gameObject);
    }
    
    private void SetSpawnPoint()
    {
        var level = instantiatedLevels[^1].transform;
        _currentNextSpawn = level.Find("NextSpawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spawn a new level if a trigger was hit
        if (!other.gameObject.CompareTag("SpawnTrigger")) return;
        SpawnElement();
        Destroy(other.gameObject);
    }
}
