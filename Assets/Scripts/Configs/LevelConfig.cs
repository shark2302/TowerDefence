using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DefenceObject
{
    [HideInInspector]
    public string UnitName;
    public GameObject SpawnedPrefab;
    public float cost;
}

[CreateAssetMenu(menuName = "Level config")]
public class LevelConfig : ScriptableObject
{

    public int Money;
    
    public bool CanSetCanon;
    public GameObject CanonPrefab;
    public int CanonCost;
    
    public bool CanSetFence;
    public GameObject FencePrefab;
    public int FenceCost;
    
    
    public int GetMoneyForLevel()
    {
        return Money;
    }

    public bool IsAvailibleToSetCanon()
    {
        return CanSetCanon;
    }


    public bool IsAvailibleToSetFence()
    {
        return CanSetFence;
    }

    public GameObject GetCanonPrefab()
    {
        return CanonPrefab;
    }

    public GameObject GetFencePrefab()
    {
        return FencePrefab;
    }

    public int GetCanonCost()
    {
        return CanonCost;
    }

    public int GetFenceCost()
    {
        return FenceCost;
    }
}