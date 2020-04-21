using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnerElement
{
    [HideInInspector]
    public string UnitName;
    public GameObject SpawnedPrefab;
    public float Delay;
}

[CreateAssetMenu(menuName = "Spawner config")]
public class SpawnerConfig : ScriptableObject
{
    public SpawnerElement[] SpawnData;
 

    public SpawnerElement GetElement(int i)
    {
        if (i < SpawnData.Length)
        {
            return SpawnData[i];
        }
        return null;
    }

    public void OnValidate()
    {
        foreach (var spawnElement in SpawnData)
        {
            if (spawnElement.SpawnedPrefab != null)
            {
                spawnElement.UnitName = spawnElement.SpawnedPrefab.name;
            }
        }
    }

}
